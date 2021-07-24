using System;
using System.Collections.Generic;
using System.Linq;
using MenuData;
using UnityEngine;

namespace OptionsMenu
{
    public class OptionsMenuModel: IOptionsMenuModel
    {
        public RectTransform PrototypeCell { get; private set; }
        public string MenuText { get; private set; }
        public Sprite MenuSprite { get; private set; }
        public bool Showing => system.Showing;
        public event EventHandler<bool> ShowingChanged;
        private MenuData.IActionMenuData MenuData;
        private readonly IOptionsMenuSystem system;
        private IOptionsListMenuData OptionsListMenuData { get; set; }
        private event EventHandler _dataChanged;
        public event EventHandler DataChanged {
            add
            {
                system.DataChanged += OnDataChanged;
                _dataChanged += value;
            }
            remove
            {
                system.DataChanged -= OnDataChanged;
                _dataChanged -= value;
            }
        }

        public OptionsMenuModel(IOptionsMenuSystem system)
        {
            Debug.Log($"OptionsMenuModel {system}");
            this.system = system;
            system.OnShowing += OnShowing;
            
        }

        private void OnShowing(object sender, bool isShowing)
        {
            Debug.Log($"OptionsMenuModel OnShowing {system.Data} ");
            this.OptionsListMenuData = system.Data;
            MenuData = system.Data.MenuData;
            PrototypeCell = system.Data.PrototypeCell;
            MenuText = system.Data.MenuText;
            MenuSprite = system.Data.MenuSprite;
            ShowingChanged?.Invoke(this,isShowing);
        }
        private void OnDataChanged(object sender, IOptionsListMenuData e)
        {
            Debug.Log($"OptionsMenuModel OnDataChanged {system.Data} ");
            this.OptionsListMenuData = e;
            MenuData = e.MenuData;
            PrototypeCell = e.PrototypeCell;
            MenuText = e.MenuText;
            MenuSprite = e.MenuSprite;
            _dataChanged?.Invoke(this, EventArgs.Empty);
        }

        

        // public OptionsMenuModel(IActionMenuData menuData, RectTransform prototypeCell, string menuText, Sprite menuSprite)
        // {
        //     MenuData = menuData;
        //     PrototypeCell = prototypeCell;
        //     MenuText = menuText;
        //     MenuSprite = menuSprite;
        // }

        public List<IOptionsListViewItem> GetOptionItems()
        {
            var  x =  MenuData.Options.Select((option) =>
            {
                var name = option.Name;
                var notifier = new OptionsNotifier(option,system);
                return (IOptionsListViewItem)new OptionsListViewItem(name,notifier);
            }).ToList();
            return x;
        }

       
    }

    public class OptionsNotifier: IOptionsItemSelectedNotifier
    {
        private readonly IOption option;
        private readonly IOptionsMenuSystem system;

        private EventHandler<bool> _event;
        public event EventHandler<bool> SelectedChanged 
        {
            add { system.OnActiveOptionChanged += SystemOnOnActiveOptionChanged;
                _event += value;
            }
            remove { system.OnActiveOptionChanged -= SystemOnOnActiveOptionChanged;
                _event -= value;
            }
        }

        public bool isSelected => system.ActiveOption == option;

        private void SystemOnOnActiveOptionChanged(object sender, IOption e)
        {
            _event?.Invoke(system,e == option);
        }

        public OptionsNotifier(IOption option,IOptionsMenuSystem system)
        {
            this.option = option;
            this.system = system;
        }
        
       
    }


    public class OptionsListViewItem : IOptionsListViewItem
    {
        private readonly IOptionsItemSelectedNotifier notifier;
        public string Name { get; }
        public bool isSelected => notifier.isSelected;

        public event EventHandler<bool> SelectedChange {
            add => notifier.SelectedChanged += value;
            remove => notifier.SelectedChanged -= value;
        }

        public OptionsListViewItem(string name, IOptionsItemSelectedNotifier notifier)
        {
            Name = name;
            this.notifier = notifier;
        }

        public override string ToString()
        {
            return $"OptionItem{{{Name},{isSelected}}}";
        }
    }

    public interface IOptionsItemSelectedNotifier
    {
        event EventHandler<bool> SelectedChanged;
        bool isSelected { get; }
    }
}