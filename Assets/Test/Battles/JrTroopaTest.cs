using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class JrTroopaTest : MonoBehaviour{
    // Start is called before the first frame update
    [Test]
    public void test_jrtroops_equals_scriptable()
    {
        // We have references to all episodes and their levels in Scriptable Object
        var jrTroop = Resources.Load<GameBattleScriptableObject>("Battles/JrTroopa");
        var battleLoader = new BattleLoader();
        Assert.NotNull(jrTroop,"jr resource load == null");
        GameBattleProvider game = new GameObject().AddComponent<GameBattleProvider>();
        Assert.NotNull(game,"game. == null");
        var expected = game.JrTroopaFirst();
        var actual = battleLoader.ConvertToBattle(jrTroop);
        Assert.AreEqual(expected: expected,actual,"battles not equal");
    }
}