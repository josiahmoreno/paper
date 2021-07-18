
using System;
using Enemies;
using MenuData;
using Scenes.ActionMenu.DataView;
using TargetSystem;
using Tests;
using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "TargetViewInstaller", menuName = "Installers/TargetViewInstaller", order = 0)]
public class TargetViewInstaller : Installer<TargetViewInstaller>
{

    [Inject]
    Enemy Enemy;
    [Inject]
    ITargetSystem TargetSystem;
    public override void InstallBindings()
    {
        Debug.Log($"TargetViewInstaller - 0 {Enemy}");
        Container.BindInstance(Enemy);
        //Container.BindInstance(TargetSystem);
        Container.BindInterfacesAndSelfTo<TargetViewPresenter>().AsTransient();
        Container.BindInterfacesAndSelfTo<TargetViewModel>().AsTransient();
        Container.BindInterfacesAndSelfTo<TargetView>().FromComponentInHierarchy().AsSingle();



    }
}




