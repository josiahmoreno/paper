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
            Model.OnSelectedChanged += OnSelectedChanged;
        }

        private void OnSelectedChanged(object sender, bool isSelected)
        {
            if (isSelected)
            {
                View.Select();
            }
            else
            {
                View.Deselect();
            }
           
        }

        public void OnStart()
        {
           
            Load(Model.MenuData);
        }

        public void OnDestroy()
        {
            Model.OnSelectedChanged -= OnSelectedChanged;
        }

        private void Load(IActionViewItem menuData)
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