using System;
using System.Collections.Generic;
using Zenject;

class ActionsMenuPresenter: IActionsMenuPresenter
{
    [Inject]
    private IActionsMenuModel model;

    public ActionsMenuPresenter(IActionsMenuModel actionMenuModel)
    {
        this.model = actionMenuModel;
    }

    public IActionsMenuView View { get; set; }

  
    public void OnStart()
    {
        View.Load(model.GetActionsData());
        model.onCharacterChange += onCharacterChange;
    }

    private void onCharacterChange(object sender, List<ActionViewItem> e)
    {
        View.Load(e);
    }
}

internal interface IActionsMenuModel
{
    List<ActionViewItem> GetActionsData();
    event EventHandler<List<ActionViewItem>> onCharacterChange;
}