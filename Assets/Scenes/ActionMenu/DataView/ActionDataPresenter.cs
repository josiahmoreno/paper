using Zenject;

namespace Scenes.ActionMenu.DataView
{
    public class ActionDataPresenter: IActionDataPresenter
    {
        public IActionDataView View { private get; set; }
        [Inject]
        private IActionDataModel Model;

        public void OnStart()
        {
            View.Load(Model.MenuData);
        }
    }
}