using System;
using System.Collections.Generic;
using MenuData;
using UnityEngine;
using Zenject;

namespace Scenes.ActionMenu.DataView
{
    public class ActionDataModel : IActionDataModel
    {
        public IActionMenu ActionMenu { get; }

        private readonly ActionViewItem data;

        public ActionDataModel(ActionViewItem data, MenuData.IActionMenu actionMenu)
        {
            Debug.Log($"MenuData {data.name} isMenuShowing {actionMenu.Showing} active {actionMenu.ActiveAction}");
            this.ActionMenu = actionMenu;
            this.data = data;
        }

        public ActionViewItem MenuData => data;

        public bool IsSelected => ActionMenu.ActiveAction == data.data;

        public object GetSprite()
        {
            throw new NotImplementedException();
        }
    }
}