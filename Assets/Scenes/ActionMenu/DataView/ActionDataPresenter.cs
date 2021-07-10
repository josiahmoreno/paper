using MenuData;
using System;
using Zenject;

namespace Scenes.ActionMenu.DataView
{
    public class ActionDataPresenter: IActionDataPresenter
    {
        private IActionDataView View;
        
        private IActionDataModel Model;

        public ActionDataPresenter(IActionDataView view,IActionDataModel model)
        {
            View = view;
            Model = model;
        }

        public void OnStart()
        {
           
            Load(Model.MenuData);
        }

        private void Load(ActionViewItem menuData)
        {
            if (Model.IsSelected)
            {
                View.Select();
                View.Load(menuData);
            } else
            {
                View.Deselect();
                View.Load(menuData);
            }
        }
    }
}