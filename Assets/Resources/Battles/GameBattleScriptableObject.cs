using System;
using System.Collections;
using System.Collections.Generic;
using Attributes;
using Battle;
using Enemies;
using Heroes;
using Items;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle", menuName = "Battles/Create Game Battle", order = 1)]
public class GameBattleScriptableObject : ScriptableObject
{
    public List<HeroInformation> Heroes;
    public List<EnemyInformation> Enemies;

    public List<BattleEventInformation> BattleEvents;
    // var bubbleSystem = new TextBubbleSystem();
    //          var Mario = new Mario(
    //              new Inventory(),
    //              new List<IJumps> { new Attacks.Jump() }.ToArray(),
    //              new Attacks.Hammer());
    //         //var Goompa = new Goompa();
    //         var goombario = new Goombario(bubbleSystem);
    //          var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
    //          var JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
    //         var JrTroopa2 = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
    //          var enemies = new List<Enemy>()
    //          {
    //              JrTroopa
    //          };
    //          var battle = new Battle.Battle(new List<Hero> { Mario, goombario }, enemies, bubbleSystem);
    //          battle.AddEventOnStarting(new TextBubbleEvent((battleEvent, battle) =>
    //          {
    //
    //              battle.ShowText(new GameText("1", "2", "3", "4"));
    //              battle.OnTextCompleted((_) => battleEvent.Complete());
    //
    //
    //          }, (battle) => battle.State == BattleState.STARTING));
    //          battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
    //          {
    //
    //              battle.ShowText(new GameText("Nice Job"));
    //              battleEvent.Completed = true;
    //              battle.OnTextCompleted((_) => battle.EndTurn());
    //
    //
    //
    //
    //          }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 4));
    //
    //          battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
    //          {
    //
    //              battle.ShowText(new GameText("Mario is lame!"));
    //              battleEvent.Completed = true;
    //              battle.OnTextCompleted((_) => battle.EndTurn());
    //              // what i return a turn end enum, then battle events haave to end turns!
    //
    //          }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 3));
    //
    //          battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
    //          {
    //
    //              battle.ShowText(new GameText("Goompa: You are almost there mario!"));
    //              battleEvent.Completed = true;
    //              battle.OnTextCompleted((_) =>
    //              {
    //                  battle.EndTurn();
    //              });
    //              // what i return a turn end enum, then battle events haave to end turns!
    //
    //          }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 2));
    //
    //          battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
    //          {
    //
    //              battle.ShowText(new GameText("Goompa: You are almost there mario!"));
    //              battleEvent.Completed = true;
    //
    //              battle.TextBubbleSystem.OnTextCompleted((_) =>
    //              {
    //                  battle.ShowText(new GameText("JrTroopa: All right, you asked for it", "Full power!!"));
    //                  battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
    //                  battle.OnTextCompleted(__ =>
    //                  {
    //                      battle.EndTurn();
    //                  });
    //              });
    //              // what i return a turn end enum, then battle events haave to end turns!
    //
    //          }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 1));
    //          battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
    //          {
    //
    //              battle.ShowText(new GameText("Goompa: You got Star points", "You get em when u win", "Every 100 you level up", "Git Hard"));
    //              battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
    //              battle.OnTextCompleted((_) =>
    //              {
    //                  battle.EndTurn();
    //              });
    //              // what i return a turn end enum, then battle events haave to end turns!
    //
    //          }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 0));
    //
    //         
    //        
    //        
    //          battle.Start();
    //          System.Diagnostics.Debug.WriteLine($"State {battle.State}");
}

[Serializable]
public class BattleEventInformation
{
    public BattleEventTrigger Trigger;
    public string EventType;
    public TextBubbleInformation TextBubbleInformation;
  
}

[Serializable]
public class BattleEventTrigger
{
    public BattleState State;
    public int Health;
    public EnemyInformation EnemyInformation;
   
}

[Serializable]
public class TextBubbleInformation
{
    public string Speaker;
    public List<string> GameTexts;
    public List<TextBubbleInformation> Dialoge;
}



[Serializable]
public class HeroInformation
{
    public string name;
    public bool Playable;
    public List<Attacks.Attacks> AttacksList;
    public List<InventoryInformation> InventoryInformation;
    
}

[Serializable]
public class InventoryInformation
{
    public string ItemInformation;
}

[Serializable]
public class EnemyInformation
{
    public string name;
    public List<Attacks.Attacks> AttacksList;
}
