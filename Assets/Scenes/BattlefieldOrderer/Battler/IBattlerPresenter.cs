namespace Scenes.BattlefieldOrderer
{
    public interface IBattlerPresenter
    {
        void OnStart();
    }

    public class BattlerPresenter: IBattlerPresenter
    {
        private IBattlerView View;
        private IBattlerModel Model;

        public BattlerPresenter(IBattlerView view, IBattlerModel model)
        {
            View = view;
            Model = model;
        }

        public void OnStart()
        {
            View.LoadSprite(Model.GetSprite());
            if (Model.Battler.Hero != null)
            {
                View.Load(Model.Battler.Hero);
            } else if (Model.Battler.Enemy != null)
            {
                View.Load(Model.Battler.Enemy);
            }
           
        }
    } 
}