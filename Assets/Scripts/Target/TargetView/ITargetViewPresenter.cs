public interface ITargetViewPresenter
{
    void OnStart();
}

public class TargetViewPresenter: ITargetViewPresenter
{
    ITargetView view;
    ITargetViewModel model;

    public TargetViewPresenter(ITargetView view, ITargetViewModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void OnStart()
    {
        model.OnTargeted += Model_OnTargeted;
        Model_OnTargeted(this, model.IsTargeted);
    }

    private void Model_OnTargeted(object sender, bool e)
    {
        if (e)
        {
            view.ShowTargeted();
        } else
        {
            view.HideTargeted();
        }
    }
}