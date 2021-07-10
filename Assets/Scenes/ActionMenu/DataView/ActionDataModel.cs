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

        public ActionViewItem MenuData => data;
    }
}