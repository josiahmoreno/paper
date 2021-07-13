using System;
using System.Collections.Generic;
using MenuData;
using Scenes.ActionMenu.DataView;
using Zenject;

class ActionsMenuPresenter: IActionsMenuPresenter
{
    [Inject]
    private IActionsMenuModel model;

    public ActionsMenuPresenter(IActionsMenuView view,IActionsMenuModel actionMenuModel)
    {
        this.View = view;
        this.model = actionMenuModel;
        this.model.OnMoveUp += (_,_) =>
        {
            view.MoveUp();
        };
        this.model.OnMoveDown += (_,_) =>
        {
            view.MoveDown();
        };
    }

    public IActionsMenuView View { get; set; }

  
    public void OnStart()
    {
        View.Load(model.GetActionsData());
        model.onCharacterChange += onCharacterChange;
    }

    private void onCharacterChange(object sender, List<IActionViewItem> e)
    {
        View.Load(e);
    }
    
    

    
}

public interface IActionsMenuModel
{
    List<IActionViewItem> GetActionsData();
    event EventHandler<List<IActionViewItem>> onCharacterChange;

    event EventHandler OnMoveUp;
    event EventHandler OnMoveDown;
}