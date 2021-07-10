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
      
    }
}