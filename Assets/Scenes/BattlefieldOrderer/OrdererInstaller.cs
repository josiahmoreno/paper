using System.Collections;
using System.Collections.Generic;
using Battle;
using Enemies;
using Heroes;
using Scenes.BattlefieldOrderer;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BattlefieldOrdererInstaller", menuName = "Installers/BattlefieldOrdererInstaller", order = 0)]
public class OrdererInstaller: ScriptableObjectInstaller<OrdererInstaller>
{
    public OrdererSettings Settings;
    [Inject] public IBattleProvider IBattleProvider;
    [Inject]
    public GameBattleScriptableObject GameBattleScriptableObject;
    public CharacterSpritesScriptableObject CharacterSpritesScriptableObject;
    public override void InstallBindings()
    {
        Debug.Log("InstallBindings");
        Container.BindInstance(IBattleProvider.Battle.TargetSystem);
        Container.Bind<ICharacterResourceProvider>().FromInstance(CharacterSpritesScriptableObject);
        Container.BindFactory<IBattler, Transform, BattlerView, BattlerSpawner.Factory>()
            .To<BattlerView>()
            .FromSubContainerResolve()
            .ByNewPrefabMethod(Settings.defaultBattlerViewPrefab,(subCon,data,trans) =>
            {
                //subCon.Install<BattlerViewInstaller>();
                Debug.Log($"Order Installer - en {data?.Enemy}");
                subCon.BindInstance(data).AsSingle();
                subCon.BindInstance(trans);
                subCon.Bind<Enemy>().FromResolveGetter<IBattler>((bat) =>
                {
                    Debug.Log($"Order Installer - getter {data?.Enemy}");
                    return bat.Enemy;
                }).WhenInjectedInto<TargetViewInstaller>();
                //subCon.BindInstance(data.Enemy).WhenInjectedInto<TargetViewInstaller>();
               
             
                TargetViewInstaller.Install(subCon);
                BattlerViewInstaller.Install(subCon);
            })
            //.ByNewPrefabInstaller<BattlerViewInstaller>(Settings.defaultBattlerViewPrefab)
            //.ByMethod((subCon ,data, transform) =>
            //{
            //    Debug.Log("FromSubContainerResolve");
            //    subCon.BindInstance(data).AsSingle();
            //    //subCon.Bind<Transform>().FromComponentInHierarchy().AsSingle();
            //    subCon.BindInterfacesAndSelfTo<BattlerModel>().AsTransient();
            //    subCon.BindInterfacesAndSelfTo<BattlerPresenter>().AsTransient();
            //    Debug.Log("FromSubContainerResolve 1");
            //    //subCon.Bind<IBattlerView>().To<BattlerView>().AsSingle();
            //    subCon.BindInterfacesAndSelfTo<BattlerView>()
            //        .FromComponentInNewPrefab(Settings.defaultBattlerViewPrefab)
            //        .UnderTransform((c) => c.Container.Resolve<BattlefieldOrderer>().transform)
            //        .AsSingle();
            //})
            .UnderTransform((c) => c.Container.Resolve<BattlefieldOrderer>().transform);
        Container.BindInterfacesAndSelfTo<BattlefieldOrderer>().FromComponentsInHierarchy().AsSingle();
        Debug.Log("InstallBindings - BattlefieldOrderer");
        //Container.Bind<IBattleProvider>().FromInstance(GameBattleScriptableObject);
        
        Container.BindInterfacesAndSelfTo<BattlefieldPositioner2>()
            .AsSingle();
            
        Container.Bind<BattlerSpawner>().AsSingle();
        
        Container.Bind<IBattlefieldOrdererPresenter>().To<BattlefieldOrdererPresenter>().AsTransient();
        Container.BindInterfacesAndSelfTo<BattlefieldOrdererModel>().AsTransient();

                    

    }
}