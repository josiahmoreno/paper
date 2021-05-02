using System;
using EntityProvider;
using Scenes.Battlefield;
using Swap;
using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] 
    GameBattleProvider _provider;
    public override void InstallBindings()
    {
        Debug.Log($"{GetType().Name} - Install Bindings");
        //Container.Bind<IInitializable>().To<GameBattleProvider>().AsSingle();
        //Container.Bind<IInitializable>().To<GameBattleProvider>().;
        Container.BindInterfacesAndSelfTo<GameBattleProvider>().FromInstance(this._provider).AsSingle().NonLazy();
        //Container.Inject(this);
        //var prov =  Container.Resolve<GameBattleProvider>();
        Container.Bind<Battle.Battle>().FromInstance(_provider.Battle).NonLazy();
        Container.Bind<IBattleFieldViewModel>().To<BattlefieldViewModel>().AsTransient();
        Container.BindInterfacesAndSelfTo<IBattlefieldPositionerImpl>().FromNewComponentOnNewGameObject()
            .AsSingle()
            .OnInstantiated(
                (context, obj) =>
                {
                    if (GameObject.Find("DefaultSpot") == null)
                    {
                        throw new Exception();
                    }
                                                                                         
                    if (obj as IBattlefieldPositionerImpl == null)
                    {
                        return;
                    }
                    (obj as IBattlefieldPositionerImpl).DefaultPosition =  GameObject.Find("DefaultSpot");
                });
        Container.Bind<ICharacterEntityProvider>().To<IEnityProviderImpl>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<SwapAnimator>().AsSingle().NonLazy();
       


    }
    
    
}