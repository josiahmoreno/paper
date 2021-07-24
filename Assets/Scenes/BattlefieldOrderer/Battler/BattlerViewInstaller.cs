using Scenes.BattlefieldOrderer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattlerViewInstaller : Installer<BattlerViewInstaller>
{
    [Inject]
    IBattler data;
    [Inject]
    Transform transform;
    public override void InstallBindings()
    {
        //Container.BindInstance(data).AsSingle();
        Container.BindInterfacesAndSelfTo<BattlerModel>().AsTransient();
        Container.BindInterfacesAndSelfTo<BattlerPresenter>().AsTransient();
        Container.BindInterfacesAndSelfTo<BattlerView>().FromComponentInHierarchy().AsSingle();
    }
}
    // Start is called before the first frame update
    
