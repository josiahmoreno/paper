using System.Collections;
using System.Collections.Generic;
using Battle;
using Heroes;
using Scenes.BattlefieldOrderer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BattlefieldOrdererInstaller", menuName = "Installers/BattlefieldOrdererInstaller", order = 0)]
public class OrdererInstaller: ScriptableObjectInstaller<OrdererInstaller>
{
    public OrdererSettings Settings;
    public GameBattleScriptableObject GameBattleScriptableObject;
    public CharacterSpritesScriptableObject CharacterSpritesScriptableObject;
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings");
        Container.Bind<ICharacterResourceProvider>().FromInstance(CharacterSpritesScriptableObject);
        Container.BindFactory<IBattler, Transform, BattlerView, BattlerSpawner.Factory>()
            .To<BattlerView>()
            .FromSubContainerResolve()
            .ByMethod((subCon ,data, transform) =>
            {
                Debug.Log("FromSubContainerResolve");
                subCon.BindInstance(data).AsSingle();
                //subCon.Bind<Transform>().FromComponentInHierarchy().AsSingle();
                subCon.BindInterfacesAndSelfTo<BattlerModel>().AsTransient();
                subCon.BindInterfacesAndSelfTo<BattlerPresenter>().AsTransient();
                Debug.Log("FromSubContainerResolve 1");
                //subCon.Bind<IBattlerView>().To<BattlerView>().AsSingle();
                subCon.BindInterfacesAndSelfTo<BattlerView>()
                    .FromComponentInNewPrefab(Settings.defaultBattlerViewPrefab)
                    .UnderTransform((c) => c.Container.Resolve<BattlefieldOrderer>().transform)
                    .AsSingle();
            });
        Container.BindInterfacesAndSelfTo<BattlefieldOrderer>().FromComponentsInHierarchy().AsSingle();
        Debug.Log("InstallBindings - BattlefieldOrderer");
        Container.Bind<Battle.Battle>().FromInstance(GameBattleScriptableObject.Battle);
        Container.BindInterfacesAndSelfTo<BattlefieldPositioner2>()
            .AsSingle();
            
        Container.Bind<BattlerSpawner>().AsSingle();
        
        Container.Bind<IBattlefieldOrdererPresenter>().To<BattlefieldOrdererPresenter>().AsTransient();
        Container.BindInterfacesAndSelfTo<BattlefieldOrdererModel>().AsTransient();

                    

    }
}