
using System;
using MenuData;
using Scenes.ActionMenu.DataView;
using UnityEngine;
using Zenject;

namespace Scenes.ActionMenu
{
    [CreateAssetMenu(fileName = "ActionMenuInstaller", menuName = "Installers/ActionMenu", order = 0)]
    public class ActionMenuInstaller : ScriptableObjectInstaller<ActionMenuInstaller>
    {
        
        public ActionMenuSettings Settings;
        public GameBattleScriptableObject GameBattleScriptableObject;
        public override void InstallBindings()
        {
            Container.BindInstance(Settings);
            //Container.Bind<IFactory<IChainElement, ChainElement>>().To(typeof(SomeOtherCommand.Factory), typeof(AnotherCommand.Factory)).FromResolve();

            //Container.BindFactory<IChainElement, SomeOtherCommand, SomeOtherCommand.Factory>();
            //Container.BindFactory<IChainElement, AnotherCommand, AnotherCommand.Factory>();
            Container.BindFactory<IActionMenuData,ActionDataModel,ActionDataModel.Factory>().AsCached();
            var a = Container.Bind<IFactory<IActionMenuData, ActionDataView>>().To<ActionDataView.Factory>().FromResolve();
            
            Container.BindFactory<IActionMenuData,ActionDataView,ActionDataView.Factory>().FromComponentInNewPrefab(Settings.ActionViewPrefab).AsCached();
            //spawner
            //Container.Bind<IActionsDataViewSpawner>().To<ActionDataView.Spawner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<IActionDataPresenter>().To<ActionDataPresenter>().AsTransient();
            
           // Container.Bind<IActionDataView>().To<ActionDataView>().FromComponentInNewPrefab(Settings.ActionViewPrefab).AsTransient();
            Container.Bind<IActionDataModel>().To<ActionDataModel>().AsTransient();
            //Container.Bind<ActionDataModel>().FromFactory<ActionDataModel.Factory>();
            //Container.Bind<Foo            >().FromIFactory(x                              => x.To<FooFactory>().FromScriptableObjectResource("FooFactory")).AsSingle();
           
           
         
            
            
            Container.Bind<IBattleProvider>().FromInstance(GameBattleScriptableObject);
            Container.Bind<IActionsMenuPresenter>().To<ActionsMenuPresenter>().AsSingle();
            Container.Bind<IActionsMenuModel>().To<ActionMenuModel>().AsSingle();
        }
    }
    
    // public class CustomEnemyFactory : IFactory<IActionMenuData,ActionDataView>
    // {
    //     DiContainer _container;
    //
    //     public CustomEnemyFactory(DiContainer container)
    //     {
    //         _container = container;
    //     }
    //
    //     public ActionDataView Create()
    //     {
    //
    //         return _container.Instantiate<ActionDataView>();
    //     }
    //
    //     public ActionDataView Create(IActionMenuData param)
    //     {
    //         return _container.Instantiate<ActionDataView>();
    //     }
    // }

    
}