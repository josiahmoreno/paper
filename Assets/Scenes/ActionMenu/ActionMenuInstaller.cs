
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
            Debug.Log("InstallBindings");
            Container.BindInstance(Settings);
           
            //Container.Bind<IActionDataPresenter>().To<ActionDataPresenter>().AsTransient();
            Container.BindFactory<ActionViewItem, Transform ,ActionDataView, ActionDataView.Factory>()
                .To<ActionDataView>()
                .FromSubContainerResolve()
                
                .ByMethod((subCon,data, parent) =>
                {
                    Transform GetParent(InjectContext context)
                    {
                        if (context.ObjectInstance is GameObject component)
                        {
                            Debug.Log($"parent name = {component.name}");
                            return component.transform;
                        }
                        Debug.Log($"parent is null");
                        return null;
                    }
                    subCon.BindInstance(data).AsSingle();
                    subCon.BindInterfacesAndSelfTo<ActionDataModel>().AsTransient();
                    subCon.BindInterfacesAndSelfTo<ActionDataPresenter>().AsTransient();
                    Debug.Log($"parent name = {parent.name}");
                    subCon.Bind<ActionDataView>()
                    .FromComponentInNewPrefab(Settings.ActionViewPrefab)
                    .UnderTransform(parent)
                    .AsSingle();
                });
            Container.Bind<ActionDataView.Spawner>().AsSingle();
            Debug.Log("111");
          
            //tworking3 HERE
            //Container.BindFactory<ITestData, TestView, TestView.Factory>().To<TestView>()
            //    .FromSubContainerResolve()
            //    .ByNewGameObjectMethod((subCon, data) =>
            //    {
            //        subCon.BindInstance(data).AsSingle();
            //        subCon.Bind<TestModel>().AsTransient();
            //        Debug.Log("222222");
            //        subCon.Bind<TestPresenter>().AsTransient();
            //        Debug.Log("44444");
            //        subCon.Bind<TestView>().FromNewComponentOnNewGameObject().AsSingle().WithArguments(data);
            //    });
            //Container.Bind<TestViewSpawner>().AsSingle();
            ////////33333

            //working5665
            //Container.BindFactory<ITestData, TestView, TestView.Factory>().To<TestView>()
            //    .FromSubContainerResolve()
            //    .ByNewGameObjectMethod((subCon, data) => {
            //        subCon.BindInstance(data).AsSingle();
            //        subCon.Bind<TestModel>().AsTransient().WhenInjectedInto<TestPresenter>();
            //        subCon.Bind<TestPresenter>().AsTransient().WithArguments(data);
            //        Debug.Log("111");
            //        var res = subCon.Instantiate<TestPresenter>();
            //        Debug.Log("222");
            //        subCon.Bind<TestView>().FromNewComponentOnNewGameObject().AsSingle().WithArguments(data, res);
            //        //subCon.Bind<Prse>(new TestPresenter());
            //        // con.Bind<TestView>(); 
            //    });
            //Container.Bind<TestViewSpawner>().AsSingle();
            ////////5464564464
            ///

            //working5665

            //Container.Bind<TestPresenter>().AsTransient().WhenInjectedInto<TestView>();
            //Container.BindFactory<ITestData, TestView, TestView.Factory>().To<TestView>().FromNewComponentOnNewGameObject() ;
         
            //Container.Bind<TestViewSpawner>().AsSingle();
            ////////5464564464

            //Container.BindFactory<IActionMenuData, ActionDataView, ActionDataView.Factory>().FromSubContainerResolve().ByNewContextPrefab<ActionDataViewInstaller>(Settings.ActionViewPrefab);
            //spawner
            //Container.Bind<IActionsDataViewSpawner>().To<ActionDataView.Spawner>().FromNewComponentOnNewGameObject().AsSingle();
            //Container.Bind<IActionDataPresenter>().To<ActionDataPresenter>().AsTransient();

            // Container.Bind<IActionDataView>().To<ActionDataView>().FromComponentInNewPrefab(Settings.ActionViewPrefab).AsTransient();
            //Container.Bind<IActionDataModel>().To<ActionDataModel>().AsTransient();
            //Container.Bind<ActionDataModel>().FromFactory<ActionDataModel.Factory>();
            //Container.Bind<Foo            >().FromIFactory(x                              => x.To<FooFactory>().FromScriptableObjectResource("FooFactory")).AsSingle();





            Container.Bind<IBattleProvider>().FromInstance(GameBattleScriptableObject);
            Container.Bind<IActionsMenuPresenter>().To<ActionsMenuPresenter>().AsSingle();
            Container.Bind<IActionsMenuModel>().To<ActionMenuModel>().AsSingle();
        }
    }

    /*
     [Test]
        public void TestConcrete()
        {
            Container.BindFactory<string, IFoo, IFooFactory>().To<Foo>().FromSubContainerResolve().ByMethod(InstallFoo).NonLazy();

            Assert.IsEqual(Container.Resolve<IFooFactory>().Create("asdf").Value, "asdf");
        }

        void InstallFoo(DiContainer subContainer, string value)
        {
            subContainer.Bind<Foo>().AsSingle().WithArgumentsExplicit(
                InjectUtil.CreateArgListExplicit(value));
        }
     * 
     */

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