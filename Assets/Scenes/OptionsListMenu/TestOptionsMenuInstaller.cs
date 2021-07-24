using System;
using System.Collections.Generic;
using System.Linq;
using MenuData;
using OptionsMenu;
using UnityEngine;
using Zenject;

public class TestOptionsMenuInstaller : MonoInstaller, IOptionsMenuSystem
{
    [SerializeField] private TestActionMenuData menuData;
    [SerializeField] private RectTransform prototypeCell;
    [SerializeField] private string menuText;
    [SerializeField] public Sprite menuSprite;
    [SerializeField] public Boolean IsShowing;
    public override void InstallBindings()
    {
      
        Container.BindInterfacesAndSelfTo<IOptionsMenuSystem>().FromInstance(this);
        // Container.BindInterfacesAndSelfTo<TestActionMenuData>().FromInstance(menuData).AsSingle();
        // //Container.BindInstance(menuData);
        // Container.BindInstance(prototypeCell);
        // Container.BindInstance(menuText);
        // Container.BindInstance(menuSprite);
        OptionsMenuInstaller.Install(Container);
    }
    
    [Serializable]
    public class TestActionMenuData : IActionMenuData
    {
        public string _name;
        public List<string> options;
        public bool Equals(IActionMenuData other)
        {
            return false;
            //throw new NotImplementedException();
        }

        public string Name => _name;

        public IOption[] Options
        {
            get => options.Select(o =>
            {
                return (IOption )new Option(o);
            }).ToArray();
        }
    }
    
    IOptionsListMenuData IOptionsMenuSystem.Data => Data;
    public bool Showing => IsShowing;

    public event EventHandler<bool> OnShowing;
    public event EventHandler<IOption> OnActiveOptionChanged;
    public IOption ActiveOption { get; }
    public event EventHandler<IOptionsListMenuData> DataChanged;
    public OptionsMenuSystem.OptionsListMenuData Data => new(menuData, prototypeCell, menuText, menuSprite);
}