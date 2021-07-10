using System.Collections;
using System.Collections.Generic;
using MenuData;
using Scenes.ActionMenu.DataView;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActionDataView : MonoBehaviour, IActionDataView
{
    [Inject] 
    public IActionDataPresenter Presenter;
    public Image Icon;
    
    // Start is called before the first frame update
    void Start()
    {
        //Presenter = new ActionDataPresenter(new ActionDataModel(new ActionViewItem(null,null,null)));
        //Presenter.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(ActionViewItem modelMenuData)
    {
        Icon.sprite = modelMenuData.sprite;
    }


    public class Spawner: IActionsDataViewSpawner
    {
        private Factory factory;

        public Spawner(ActionDataView.Factory factory)
        {
            this.factory = factory;
        }

        public GameObject InstantiateView(ActionViewItem data,GameObject dataActionItemPrefab, Transform transform)
        {

            var game =  factory.Create(data,transform).gameObject ;
            //game.transform.SetParent(transform);
            return game;
        }
    }

    public class Factory : PlaceholderFactory<ActionViewItem, Transform,ActionDataView>
    {
        
    }
}

public interface IActionDataPresenter
{
    void OnStart();
}

public interface IActionDataView
{
    void Load(ActionViewItem modelMenuData);
}
