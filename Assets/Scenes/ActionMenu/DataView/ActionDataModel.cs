using System;
using System.Collections.Generic;
using MenuData;
using UnityEngine;
using Zenject;

namespace Scenes.ActionMenu.DataView
{
    public class ActionDataModel : IActionDataModel
    {
        private readonly ActionViewItem data;

        public ActionDataModel(ActionViewItem data)
        {
            this.data = data;
        }

        public class Factory: PlaceholderFactory<IActionMenuData,ActionDataModel>
        {
            [Inject] private readonly ActionMenuSettings Settings;

            // public ActionDataModel Create(IActionMenuData param)
            // {
            //     throw new NotImplementedException();
            // }
            
            public override ActionDataModel Create(IActionMenuData param)
            {
                Debug.Log("Wtf model factory");
                return new ActionDataModel( new ActionViewItem(param,Settings.ResourceProvider.GetSpriteForMenuData(param),Settings.ActionViewPrefab));
            }
        }

        public ActionViewItem MenuData => data;
    }
}