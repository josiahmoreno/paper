using System.Collections;
using System.Collections.Generic;
using Enemies;
using MenuData;
using Moq;
using NUnit.Framework;
using PaperLib.Enemies;
using TargetSystem;
using TargetSystem2;
using UnityEngine;
using UnityEngine.TestTools;

public class TargetSystemModelTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void test_subbed()
    {
        var enemies = new List<Enemy>();
        var system = new Mock<ITargetSystem>();
        TargetInformationArgs targetInformation = null;
        system.SetupProperty(s => s.ActiveChanged);
        var model = new TargetSystemModel(system.Object,enemies);
        model.OnShowingTargetInformation += (sender, args) =>
        {
            targetInformation = args;
        };
        system.Object.ActiveChanged(enemies.ToArray());
        Assert.NotNull(targetInformation);
    }

    [Test]
    public void test_show_all()
    {
        var fac = new EnemyFactory();
        var goomba1 = fac.FetchEnemy<NewGoomba>();
        var goomba2 = fac.FetchEnemy<NewGoomba>();
        var enemies = new List<Enemy>() {goomba1, goomba2 };
        var system = new Mock<ITargetSystem>();
        TargetInformationArgs targetInformation = null;
        system.SetupProperty(s => s.ActiveChanged);
        var model = new TargetSystemModel(system.Object, enemies);
        model.OnShowingTargetInformation += (sender, args) =>
        {
            targetInformation = args;
        };
        system.Object.ActiveChanged(enemies.ToArray());
        Assert.AreEqual(TargetType.All, targetInformation.TargetType);
    }

    [Test]
    public void test_show_single()
    {
        var fac = new EnemyFactory();
        var goomba1 = fac.FetchEnemy<NewGoomba>();
        var goomba2 = fac.FetchEnemy<NewGoomba>();
        var enemies = new List<Enemy>() { goomba1, goomba2 };
        var system = new Mock<ITargetSystem>();
        TargetInformationArgs targetInformation = null;
        system.SetupProperty(s => s.ActiveChanged);
        var model = new TargetSystemModel(system.Object, enemies);
        model.OnShowingTargetInformation += (sender, args) =>
        {
            targetInformation = args;
        };
        system.Object.ActiveChanged(enemies.GetRange(0,1).ToArray());
        Assert.AreEqual(TargetType.Single, targetInformation.TargetType);
    }


}
