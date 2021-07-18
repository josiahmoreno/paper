using System.Collections;
using System.Collections.Generic;
using Enemies;
using Moq;
using NUnit.Framework;
using PaperLib.Enemies;
using TargetSystem;
using UnityEngine;
using UnityEngine.TestTools;

public class TargetViewTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TargetView_selectionchange()
    {
        var targetSystem = new Mock<ITargetSystem>();
        var fac = new EnemyFactory();
        var enemy = fac.FetchEnemy<NewGoomba>();
     
        bool wasSelectedChange = false;
        targetSystem.SetupProperty(t => t.ActiveChanged);
        var model = new TargetViewModel(enemy, targetSystem.Object);
        model.OnTargeted += (a, b) => wasSelectedChange = b;
        targetSystem.Object.ActiveChanged(new Enemy[] { enemy });
        Assert.IsTrue(wasSelectedChange);
        targetSystem.Object.ActiveChanged(new Enemy[] {  });
        Assert.IsFalse(wasSelectedChange);
        targetSystem.Object.ActiveChanged(new Enemy[] { fac.FetchEnemy<NewGoomba>()});
        Assert.IsFalse(wasSelectedChange);
    }

   
}
