using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TargetSystem;
using UnityEngine;
using Zenject;

public class TargetViewModel : ITargetViewModel
{
    private readonly Enemy Enemy;
    private ITargetSystem TargetSystem;

    //public TargetViewModel( ITargetSystem targetSystem)
    //{
    //    Debug.Log($"TargetViewModel - 0 ");
    //    TargetSystem = targetSystem;
    //    TargetSystem.ActiveChanged += TargetActiveChanged;
    //}

    public TargetViewModel([InjectOptional] Enemy? enemy, ITargetSystem targetSystem)
    {
        Debug.Log($"TargetViewModel - 1 enemy = ({enemy})");
        this.Enemy = enemy;
        TargetSystem = targetSystem;
        TargetSystem.ActiveChanged += TargetActiveChanged;
    }

    public bool IsTargeted => CheckIsTargeted(TargetSystem.Actives);

    public event EventHandler<bool> OnTargeted;

    private void TargetActiveChanged(Enemy[] obj)
    {
        OnTargeted?.Invoke(this, CheckIsTargeted(obj));
    }

    public bool CheckIsTargeted(Enemy[] obj)
    {
        return (obj != null && obj.ToList().Contains(Enemy));
        
    }
}
