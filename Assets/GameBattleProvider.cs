using System;
using System.Collections.Generic;
using System.Linq;
using Attacks;
using Battle;
using Enemies;
using Heroes;
using Items;
using MenuData;
using UnityEngine;
using Zenject;

public class GameBattleProvider: MonoBehaviour, IInitializable
{
    public enum MockBattles
    {
        JrTroopaFirst,
        GoombaKingFirst
    }

    public Battle.Battle CustomBattle;
    
    public Battle.Battle Battle
    {
        get
        {
            switch (Boss)
            {
                case MockBattles.GoombaKingFirst:
                    return GoombaKing();
                    break;
                default: case MockBattles.JrTroopaFirst:
                    return JrTroopaFirst();
                    break;
            }
        }

        //}
       // set;
    }

    public MockBattles Boss;
    private void Awake()
    {
        Debug.Log($"{GetType().Name} - Awake");
        switch (Boss)
        {
           case MockBattles.GoombaKingFirst:
               GoombaKing();
               break;
           case MockBattles.JrTroopaFirst:
               JrTroopaFirst();
               break;
        }
    }
    
    

    public Battle.Battle JrTroopaFirst()
    {
        var bubbleSystem = new TextBubbleSystem();
            var Mario = new Mario(
                new Inventory(),
                new List<IJumps> { new Attacks.Jump() }.ToArray(),
                new Attacks.Hammer());
           var Goompa = new Goompa();
           //var goombario = new Goombario(bubbleSystem);
            var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
            var JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
            var enemies = new List<Enemy>()
            {
                JrTroopa
            };
            var battle = new Battle.Battle(new List<Hero> { Mario, Goompa }, enemies, bubbleSystem);
            battle.AddEventOnStarting(new Battle.TextBubbleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("1", "2", "3", "4"));
                battle.OnTextCompleted((_) => battleEvent.Complete());


            }, (battle) => battle.State == BattleState.STARTING));
            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Nice Job"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) => battle.EndTurn());




            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 4));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Mario is lame!"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) => battle.EndTurn());
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 3));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 2));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;

                battle.TextBubbleSystem.OnTextCompleted((_) =>
                {
                    battle.ShowText(new GameText("JrTroopa: All right, you asked for it", "Full power!!"));
                    battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                    battle.OnTextCompleted(__ =>
                    {
                        battle.EndTurn();
                    });
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 1));
            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You got Star points", "You get em when u win", "Every 100 you level up", "Git Hard"));
                //battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                battle.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 0));

           
          
          
            battle.Start();
            System.Diagnostics.Debug.WriteLine($"State {battle.State}");
            return battle;
    }

    public Battle.Battle GoombaKing()
    {
        var bubbleSystem = new TextBubbleSystem();
        var mario = new Mario(
            new Inventory(new Item("Mushroom"), new Item("Fire Flower", 3, TargetType.All), new Item("Mushroom")),
            new List<IJumps> { new Attacks.Jump(), new PowerJump() }.ToArray(),
            new Attacks.Hammer());
        var goombario = new Goombario(bubbleSystem);
        var GoombaKing = new GoombaKing(new List<IEnemyAttack> { new ScriptAttack(EnemyAttack.GoomnutJump), new GoombaKingKick() });
        var goomNutTree = new GoomnutTree();
        var enemies = new List<Enemy>()
        {
            goomNutTree,
            GoombaKing,
            new RedGoomba(2),
            new BlueGoomba(2),
        };
        var battle = new Battle.Battle(new List<Hero> { mario, goombario }, enemies, bubbleSystem);
        battle.Start();
        return battle;
    }


    public void Initialize()
    {
        Debug.Log($"{GetType().Name} -Initialize");
        switch (Boss)
        {
            case MockBattles.GoombaKingFirst:
                GoombaKing();
                break;
            case MockBattles.JrTroopaFirst:
                JrTroopaFirst();
                break;
        }
    }
}