using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;

public class BattleLoaderTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void BattleLoaderTestsSimplePasses()
    {
        var loader = new BattleLoader();
        var scriptable = new Mock<GameBattleScriptableObject>(MockBehavior.Loose);
        scriptable.Object.BattleEvents = new List<BattleEventInformation>();
        scriptable.Object.Heroes = new List<HeroInformation>() { new HeroInformation(){
        name = "Mario", AttacksList = new List<Attacks.Attacks>()
        {
            Attacks.Attacks.BaseJump, Attacks.Attacks.BaseHammer
        }
        } };
        scriptable.Object.Enemies = new List<EnemyInformation>()
        {
            new EnemyInformation()
            {
                name = "JrTroopa"
            },
             new EnemyInformation()
            {
                name = "Goomba"
            },
             new EnemyInformation()
             {
                 name = "Goomba"
             },
             new EnemyInformation()
             {
                 name = "Goomba"
             }
        };
    
        //scriptable.Setup(b => ).Returns(new List<HeroInformation>());
        var battle = loader.ConvertToBattle(scriptable.Object);
        Assert.AreEqual(4, battle.Enemies.Count);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BattleLoaderTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
