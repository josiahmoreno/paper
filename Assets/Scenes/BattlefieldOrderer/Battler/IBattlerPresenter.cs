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
        }
    } 
}