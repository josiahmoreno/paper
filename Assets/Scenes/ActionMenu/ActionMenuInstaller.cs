
using System;
using MenuData;
using Scenes.ActionMenu.DataView;
using Tests;
using UnityEngine;
using Zenject;

namespace Scenes.ActionMenu
{
    [CreateAssetMenu(fileName = "ActionMenuInstaller", menuName = "Installers/ActionMenu", order = 0)]
    public class ActionMenuInstaller : ScriptableObjectInstaller<ActionMenuInstaller>
    {

        public ActionMenuSettings Settings;
        [Inject] public IBattleProvider BattleProvider;
        [Inject]
        public GameBattleScriptableObject GameBattleScriptableObject;
      
        public override void InstallBindings()
        {
            Debug.Log("InstallBindings");
            Container.BindInstance(Settings);
            Container.BindFactory<IActionViewItem, Transform, ActionDataView, ActionDataView.Factory>()
                .To<ActionDataView>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<ActionDataInstaller>(Settings.ActionViewPrefab)
                .UnderTransform((injectContext) =>
                {
                    var t = injectContext.Container.Resolve<ActionsMenu>().transform;
                    return t;
                });
                ;
                //.ByMethod((subCon,data, parent) =>
                //{
                //    subCon.BindInstance(data).AsSingle();
                //    subCon.BindInterfacesAndSelfTo<ActionDataModel>().AsTransient();
                //    subCon.BindInterfacesAndSelfTo<ActionDataPresenter>().AsTransient();
                //    Debug.Log($"parent name = {parent?.name}");
                //    subCon.BindInterfacesAndSelfTo<ActionDataView>()
                //    .FromComponentInNewPrefab(Settings.ActionViewPrefab)
                //    .UnderTransform(parent)
                //    .AsSingle();
                //});
            Container.Bind<ActionDataView.Spawner>().AsSingle();
            //Container.Bind<IBattleProvider>().FromInstance(GameBattleScriptableObject);
            Container.Bind<MenuData.IActionMenu>().FromInstance(BattleProvider.Battle.ActionMenu);
            Container.Bind<ITurnSystem>().FromInstance(BattleProvider.Battle.TurnSystem);
            Container.BindInterfacesAndSelfTo<ActionsMenu>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<TestMenuChanger>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IActionsMenuPresenter>().To<ActionsMenuPresenter>().AsTransient();
            Container.Bind<IActionsMenuModel>().To<ActionMenuModel>().AsTransient();

                    

        }
    }

   


}