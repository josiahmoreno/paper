using UnityEngine.UI;

namespace OptionsMenu
{
    public class OptionsMenuPresenter: IOptionsMenuPresenter
    {
        private IOptionsMenuModel Model;
        private IOptionsMenu2View View;

        public OptionsMenuPresenter(IOptionsMenuModel model, IOptionsMenu2View view)
        {
            Model = model;
            View = view;
        }

        public void OnStart()
        {
            Model.ShowingChanged += (sender, b) =>
            {
                OnShowing();
            };
            Model.DataChanged += (sender, args) =>
            {
                OnShowing();
            };
                OnShowing();
            

            
            
            
        }

        private void OnShowing()
        {
            if (Model.Showing)
            {
                View.Show();
                View.SetPrototypeCell(Model.PrototypeCell);
                View.SetMenuIcon(Model.MenuSprite);
                View.SetMenuText(Model.MenuText);
                View.Load(Model.GetOptionItems());
                
            }
            else
            {
                View.Hide(); 
            }
        }
    }
}