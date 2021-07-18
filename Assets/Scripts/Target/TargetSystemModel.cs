using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TargetSystem;
using TargetSystem2;
using UnityEngine;

public class TargetSystemModel : ITargetSystemModel
{
    public event EventHandler<TargetInformationArgs> OnShowingTargetInformation;
    private TargetSystem.ITargetSystem targetSystem;
    private List<Enemy> enemies;

    public TargetSystemModel(ITargetSystem targetSystem, List<Enemy> enemies)
    {
        this.targetSystem = targetSystem;
        this.enemies = enemies;
        this.targetSystem.ActiveChanged += OnActiveChanged;
    }

    private void OnActiveChanged(Enemy[] obj)
    {
        if (Enumerable.SequenceEqual(enemies, obj))
        {
            OnShowingTargetInformation?.Invoke(this,new TargetInformationArgs(MenuData.TargetType.All));
        } else
        {
            OnShowingTargetInformation?.Invoke(this, new TargetInformationArgs(MenuData.TargetType.Single));
        }
    }
}
