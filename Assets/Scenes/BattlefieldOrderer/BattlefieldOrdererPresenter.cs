namespace Scenes.BattlefieldOrderer
{
    public class BattlefieldOrdererPresenter: IBattlefieldOrdererPresenter
    {
        private IBattlefieldOrdererView view;
        private IBattlefieldOrdererModel model;

        public BattlefieldOrdererPresenter(IBattlefieldOrdererView view, IBattlefieldOrdererModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void OnStart()
        {
            this.view.LoadMario(model.GetMario());
            this.view.LoadCharacters(model.GetBattlers());
        }
    }
}