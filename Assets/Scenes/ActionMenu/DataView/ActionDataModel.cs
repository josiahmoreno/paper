using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MenuData;
using UnityEngine;
using Zenject;

namespace Scenes.ActionMenu.DataView
{
    public class ActionDataModel : IActionDataModel
    {
        public IActionMenu ActionMenu { get; }

        private readonly IActionViewItem data;

        public ActionDataModel(IActionViewItem data, MenuData.IActionMenu actionMenu)
        {
            //Debug.Log($"MenuData {data.name} isMenuShowing {actionMenu.Showing} active {actionMenu.ActiveAction}");
            this.ActionMenu = actionMenu;
            actionMenu.OnActiveActionChanged += (sender, menuData) => OnSelectedChanged?.Invoke(this, IsSelected);
            this.data = data;
        }

        public IActionViewItem MenuData => data;
        public bool IsSelected => ActionMenu.ActiveAction == data.data;
        public event EventHandler<bool> OnSelectedChanged;

        public object GetSprite()
        {
            throw new NotImplementedException();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}