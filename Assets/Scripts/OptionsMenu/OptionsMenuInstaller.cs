using MenuData;
using UnityEngine;
using Zenject;

namespace OptionsMenu
{
    public class OptionsMenuInstaller: Installer<OptionsMenuInstaller>
    {
        [Inject] IOptionsMenuSystem System;
        // [Inject] private IActionMenuData menuData;
        // [Inject] private RectTransform prototypeCell;
        // [Inject] private string menuText;
        // [Inject] private Sprite menuSprite;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<OptionsMenuPresenter>().AsTransient();
            Container.BindInterfacesAndSelfTo<OptionsMenuModel>().AsTransient();
            Container.BindInterfacesAndSelfTo<OptionsMenuView2>().FromComponentInHierarchy().AsSingle();
        }
    }
}