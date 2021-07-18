using System;
using System.Collections.Generic;
using System.Linq;
using Attacks;
using Battle;
using Heroes;
using Items;
using PaperLib.Enemies;
using Enemy = Enemies.Enemy;

public class BattleLoader
{
    public Battle.Battle ConvertToBattle(GameBattleScriptableObject jrTroop)
    {
        var bubbleSystem = new TextBubbleSystem();
        var heroes = new List<Heroes.Hero>();
        var enemies = new List<Enemy>();
        jrTroop.Heroes.ForEach(hero =>
        {
            if (hero.name == "Mario")
            {
                IAttack[] jumps = toJumps(hero.AttacksList);
                IAttack[] hammers = toHammers(hero.AttacksList);
                var Mario = new Mario(
                    toInventory(hero.InventoryInformation),
                    jumps,
                    hammers);
                heroes.Add(Mario);
            }

            if (hero.name == "Goombario")
            {
                var goombario = new Goombario(bubbleSystem);
                heroes.Add(goombario);
            }
            if (hero.name == "Goompa")
            {
                var goompa = new Goompa();
                heroes.Add(goompa);
            }

        });
        jrTroop.Enemies.ForEach(enemy =>
        {


            if (enemy.name == "JrTroopa")
            {
                var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
                var JrTroopa = new Enemies.JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
                enemies.Add(JrTroopa);
            }
            if(enemy.name == "Goomba")
            {
                enemies.Add(new EnemyFactory().FetchEnemy<NewGoomba>());
            }

        });
        var Mario = new Mario(
                new Inventory(),
                new List<IJumps> { new Attacks.Jump() }.ToArray(),
                new Attacks.Hammer());
        //var Goompa = new Goompa();
        var goombario = new Goombario(bubbleSystem);
        var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
        var JrTroopa = new Enemies.JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
        var enemies22 = new List<Enemy>()
            {
                JrTroopa
            };
        var battle = new Battle.Battle(heroes, enemies, bubbleSystem);
        Predicate<Battle.Battle> ifStartingPredicate = (battle) => battle.State == BattleState.STARTING;

        jrTroop.BattleEvents.ForEach(ev =>
        {
            Predicate<Battle.Battle> pred = null;
            if (ev.Trigger.State == BattleState.STARTING)
            {
                pred = ifStartingPredicate;
                if (ev.EventType == "textbubble")
                {
                    battle.AddEventOnStarting(new Battle.TextBubbleEvent((battleEvent, battle) =>
                    {
                        if (ev.textBubbleEvent.dialogue.Count == 0)
                        {
                            battle.ShowText(new GameText(ev.textBubbleEvent.dialogue[0].GameTexts.ToArray()));
                            battle.OnTextCompleted((_) => battleEvent.Complete());
                        }
                       
                    }, battle => pred.Invoke(battle)));
                }

            }
            else
            {
                if (ev.Trigger.EnemyInformation != null)
                {
                    var enemyId = ev.Trigger.EnemyInformation.name;
                    var health = ev.Trigger.Health;
                    pred = (battle) => battle.Enemies.First(enemy => enemy.Identifier == enemyId).Health.CurrentValue == health;
                }
                if (ev.EventType == "textbubble")
                {
                    battle.AddEventOnStart(new Battle.TextBubbleEvent((battleEvent, battle) =>
                    {
            
                        
                        battle.ShowText(new GameText(ev.textBubbleEvent.dialogue[0].GameTexts.ToArray()));
                        battleEvent.Completed = true;
                        if (ev.textBubbleEvent.dialogue.Count == 1)
                        {
                            battle.OnTextCompleted((_) => battle.EndTurn());
                        } else
                        {
                            battle.TextBubbleSystem.OnTextCompleted((_) =>
                            {
                                battle.ShowText(new GameText(ev.textBubbleEvent.dialogue[1].GameTexts.ToArray()));
                                battle.OnTextCompleted(__ =>
                                {
                                    battle.EndTurn();
                                });
                            });
                        }
                       


                    }, battle => pred.Invoke(battle)));
                }
            }




        });

        battle.Start();

        return battle;
    }

    private IAttack[] toHammers(List<Attacks.Attacks> heroAttacksList)
    {
        var list = new List<IAttack>();
        heroAttacksList.ForEach(att =>
        {
            if (att is Attacks.Attacks.BaseHammer)
            {
                list.Add(new Hammer());
            }
            if (att is Attacks.Attacks.HammerThrow)
            {
                list.Add(new HammerThrow());
            }

        });
        return list.ToArray();
    }

    private IAttack[] toJumps(List<Attacks.Attacks> heroAttacksList)
    {
        var list = new List<IAttack>();
        heroAttacksList.ForEach(att =>
        {
            if (att is Attacks.Attacks.BaseJump)
            {
                list.Add(new Jump());
            }
            if (att is Attacks.Attacks.PowerJump)
            {
                list.Add(new PowerJump());
            }

        });
        return list.ToArray();
    }

    private Inventory toInventory(List<InventoryInformation> heroInventoryInformation)
    {
        return new Inventory();
    }
}