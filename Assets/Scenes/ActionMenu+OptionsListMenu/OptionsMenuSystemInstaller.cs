using OptionsMenu;
using UnityEngine;
using Zenject;

public class OptionsMenuSystemInstaller : MonoInstaller
{
    [Inject] private IBattleProvider Provider2;
    [SerializeField] private ActionMenuResourcesScriptableObject _provider;
    [SerializeField] private RectTransform PrototypeCell;
    
    public override void InstallBindings()
    {
        Container.BindInstance(Provider2.Battle.OptionsListMenu);
        Container.BindInterfacesAndSelfTo<OptionsMenuSystem>().AsSingle().WithArguments(Provider2.Battle.ActionMenu,Provider2.Battle.OptionsListMenu, PrototypeCell, _provider);
        OptionsMenuInstaller.Install(Container);
    }
}