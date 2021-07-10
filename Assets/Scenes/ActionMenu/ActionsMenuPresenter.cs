using System;
using System.Collections.Generic;
using MenuData;
using Scenes.ActionMenu.DataView;
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

public interface IActionsMenuModel
{
    List<ActionViewItem> GetActionsData();
    event EventHandler<List<ActionViewItem>> onCharacterChange;
}