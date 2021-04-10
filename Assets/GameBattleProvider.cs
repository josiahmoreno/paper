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

public class GameBattleProvider: MonoBehaviour
{
    public enum MockBattles
    {
        JrTroopaFirst,
        GoombaKingFirst
    }
    public Battle.Battle Battle { get; set; }
    public MockBattles Boss;
    private void Awake()
    {
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

    public void JrTroopaFirst()
    {
        var bubbleSystem = new TextBubbleSystem();
            var Mario = new Mario(
                new Inventory(),
                new List<IJumps> { new Attacks.Jump() }.ToArray(),
                new Attacks.Hammer());
           var Goompa = new Goompa();
            var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
            var JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
           var JrTroopa2 = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
            var enemies = new List<Enemy>()
            {
                JrTroopa
            };
            Battle = new Battle.Battle(new List<Hero> { Mario, Goompa }, enemies, bubbleSystem);
            Battle.AddEventOnStarting(new TextBubbleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("1", "2", "3", "4"));
                battle.OnTextCompleted((_) => battleEvent.Complete());


            }, (battle) => battle.State == BattleState.STARTING));
            Battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Nice Job"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) => battle.EndTurn());




            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 4));

            Battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Mario is lame!"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) => battle.EndTurn());
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 3));

            Battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 2));

            Battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
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
            Battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You got Star points", "You get em when u win", "Every 100 you level up", "Git Hard"));
                battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                battle.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 0));

           
          
          
            Battle.Start();
            System.Diagnostics.Debug.WriteLine($"State {Battle.State}");
    }

    public void GoombaKing()
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
        this.Battle = new Battle.Battle(new List<Hero> { mario, goombario }, enemies, bubbleSystem);
        Battle.Start();
    }
    
    
}