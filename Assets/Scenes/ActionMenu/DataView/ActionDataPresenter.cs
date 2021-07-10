using MenuData;
using Zenject;

namespace Scenes.ActionMenu.DataView
{
    public class ActionDataPresenter: IActionDataPresenter
    {
        public IActionDataView View { private get; set; }
        
        private IActionDataModel Model;

        public ActionDataPresenter(IActionDataModel model)
        {
            Model = model;
        }

        public void OnStart()
        {
            View.Load(Model.MenuData);
        }
        
        public class Factory : PlaceholderFactory<IActionMenuData,ActionDataPresenter>
        {
            private DiContainer _container;
            private ActionDataModel.Factory modelFactory;

            public Factory(DiContainer container, ActionDataModel.Factory modelFactory)
            {
                _container = container;
                this.modelFactory = modelFactory;
            }

            public override ActionDataPresenter Create(IActionMenuData menuData)
            {
                return new ActionDataPresenter(modelFactory.Create(menuData));
            }
        }
    }
}