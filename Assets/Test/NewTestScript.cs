using System.Collections;
using System.Collections.Generic;
using Attacks;
using Enemies;
using Heroes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.TestTools;
using Battle = Battle.Battle;
using Battler = Battle.Battle;

public class NewTestScript
{
    private TargetSystemView _targetSystemView;

    private GameBattleProvider _gameBattleProvider;
    // A Test behaves as an ordinary method
    
    
    [Test]
    public void HammerTest()
    {
        _targetSystemView = new GameObject().AddComponent<TargetSystemView>();
        _gameBattleProvider = _targetSystemView.gameObject.AddComponent<GameBattleProvider>();
        var battler =  new Battler();
        var goomba1 = new Goomba();
        var goomba2 = new Goomba();
        var goomba3 = new Goomba();
        battler.Enemies = new List<Enemy>(){goomba1, goomba2, goomba3};
        battler.Heroes = new List<Hero>() {new Mario()};
        battler.Start();
        battler.TargetSystem.Show(battler.ActionMenu.Items[3].Options[0]);
        _targetSystemView.Provider = _gameBattleProvider;
        _gameBattleProvider.CustomBattle = battler;
        var targetSystem = _gameBattleProvider.CustomBattle.TargetSystem;
        
        _targetSystemView.Provider = _gameBattleProvider;
        targetSystem.MoveTargetRight();
        Assert.True(targetSystem.Actives[0] == goomba1);
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
