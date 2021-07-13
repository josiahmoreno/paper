using System;
using System.Collections;
using System.Collections.Generic;
using Heroes;
using MenuData;
using Moq;
using NUnit.Framework;
using Scenes.ActionMenu.DataView;
using Tests;
using UnityEngine;
using UnityEngine.TestTools;

public class ActionMenuModelTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void test_onActiveChanged()
    {
        var turn = new Mock<ITurnSystem>();
        //var provider = new Mock<IBattleProvider>();
        //var battle = new Mock<Battle.Battle>();
        //battle.Setup(b => b.TurnSystem).Returns(turn.Object);
        //battle.tur
            //provider.Setup(p => p.Battle).Returns(battle.Object);
            var menu = new Mock<IActionMenu>();
        var mover = new Mock<IMenuMovementChangeProvider>();
        var hero = new Mock<Hero>();
        hero.Setup(h => h.Actions).Returns(Array.Empty<IActionMenuData>() );
        bool onMenuChanged = false;

        void Sub( object b)
        {
            Debug.Log("yo");
            onMenuChanged = true;
        }
        turn.SetupProperty(x => x.OnActiveChanged);
        var model = new ActionMenuModel(turn.Object,menu.Object,mover.Object);
        model.onCharacterChange += (a,b) => onMenuChanged = true ;
        //turn.Raise(a => a.OnActiveChanged += Sub,null,null);
        Action<object> a = (obj) => { Console.WriteLine("Testing OnClose."); };
        // possible Moq setup
       
        //turn.Setup(t => t.OnActiveChanged).Returns(a);
        turn.Object.OnActiveChanged(hero.Object);
        Assert.IsTrue(onMenuChanged);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ActionMenuModelTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
