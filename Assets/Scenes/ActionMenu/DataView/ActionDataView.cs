using System;
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
    public Image Background;
    // Start is called before the first frame update
    void Start()
    {
        
        Presenter.OnStart();
    }

    private void OnDestroy()
    {
        Presenter.OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(IActionViewItem modelMenuData)
    {
        Icon.sprite = modelMenuData.sprite;
    }

    public void Deselect()
    {
        Background.enabled = false ;
    }

    public void Load(Sprite sprite)
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        //Background.color = Color.red;
        Background.enabled = true;
    }

    public class Spawner: IActionsDataViewSpawner
    {
        private Factory factory;

        public Spawner(ActionDataView.Factory factory)
        {
            this.factory = factory;
        }

        public GameObject InstantiateView(IActionViewItem data,GameObject dataActionItemPrefab, Transform transform)
        {

            var game =  factory.Create(data,transform).gameObject ;
            return game;
        }
    }

    public class Factory : PlaceholderFactory<IActionViewItem, Transform,ActionDataView>
    {
        
    }
}

public interface IActionDataPresenter
{
    void OnStart();
    void OnDestroy();
}

public interface IActionDataView
{
    void Deselect();
    void Load(IActionViewItem modelMenuData);
    void Load(Sprite sprite);
    void Select();
}
