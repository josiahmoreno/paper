using System;
using System.Collections;
using System.Collections.Generic;
using MenuData;
using OptionsMenu;
using UnityEngine;

public class OptionsMenuSystem : IOptionsMenuSystem
{
       private IOptionsListMenu MainListMenu;
       private IActionMenu ActionMenu;
       private readonly RectTransform prototypeCell;
       private readonly IResourceProvider _provider;

       public OptionsMenuSystem(IActionMenu actionMenu,IOptionsListMenu mainListMenu, RectTransform prototype, IResourceProvider provider)
       {
              this._provider = provider;
              this.ActionMenu = actionMenu;
              this.prototypeCell = prototype;
              MainListMenu = mainListMenu;
              //SubListMenu = subListMenu;
              
              MainListMenu.OnShowing += OnMenuShowing;
       }

 

       private void OnMenuShowing(bool obj)
       {
              Debug.Log($"{GetType()} - MainListMenu/{MainListMenu.Active} ActionMenu/{ActionMenu.ActiveAction}");
              OnShowing?.Invoke(this, obj);
       }

       public class OptionsListMenuData : IOptionsListMenuData
       {
              public OptionsListMenuData(IActionMenuData menuData, RectTransform prototypeCell, string menuText,
                     Sprite menuSprite)
              {
                     MenuData = menuData;
                     PrototypeCell = prototypeCell;
                     MenuText = menuText;
                     MenuSprite = menuSprite;
              }

              public MenuData.IActionMenuData MenuData { get; set; }
              public RectTransform PrototypeCell { get; set; }
              public string MenuText { get; set; }
              public Sprite MenuSprite { get; set; }

              public override string ToString()
              {
                     return $"OptionsListMenuData{{{MenuData.Name}, {MenuText}}}";
              }
       }

       public event EventHandler<IOptionsListMenuData> DataChanged;
       // {
       //        add
       //        {
       //               ActionMenu.ActiveAction 
       //        }
       //        remove
       //        {
       //               
       //        }
       // }

       public IOptionsListMenuData Data {
              get
              {
                     Debug.Log($"{GetType().Name} Data get; actionMenu {ActionMenu}");
                     return FetchData(ActionMenu.ActiveAction);
              }
       }

       public bool Showing => MainListMenu.Showing;

       public event EventHandler<bool> OnShowing;
       public event EventHandler<IOption> OnActiveOptionChanged
       {
              add => MainListMenu.OnActiveChanged += value;
              remove => MainListMenu.OnActiveChanged -= value;
       }

       public IOption ActiveOption => MainListMenu.Active;

       private IOptionsListMenuData FetchData(IActionMenuData actionMenuData)
       {
              var sprite = _provider.GetSpriteForMenuData(null, actionMenuData);
              var text = _provider.GetTextForMenuData(actionMenuData);
              return new OptionsListMenuData(actionMenuData, prototypeCell, text, sprite);
       }
       //public Data Data { get; }
}

internal class FetchableMenuData : IOptionsListMenuData
{
       private IResourceProvider _provider;

       public FetchableMenuData(IResourceProvider provider, IActionMenuData menuData, RectTransform prototypeCell)
       {
              _provider = provider;
              MenuData = menuData;
              PrototypeCell = prototypeCell;
       }

       public IActionMenuData MenuData { get; set; }
       public RectTransform PrototypeCell { get; set; }
       public string MenuText { get; set; }
       public Sprite MenuSprite
       {
              get => _provider.GetSpriteForMenuData(null, MenuData);
              set { }
       }
}
