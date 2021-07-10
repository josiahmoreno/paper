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
        Presenter.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(ActionViewItem modelMenuData)
    {
        Icon.sprite = modelMenuData.sprite;
    }


    public class Spawner: MonoBehaviour, IActionsDataViewSpawner, IFactory
    {
        //[Inject] 
       // private Factory _factory;

        private DiContainer _container;

        public Spawner(DiContainer container)
        {
            _container = container;
          //  _factory = factory;
        }

        public GameObject InstantiateView(IActionMenuData data,GameObject dataActionItemPrefab, Transform transform)
        {
            Debug.Log($"yoooo instan view container == null{ _container == null}");
            var view = _container.Instantiate<ActionDataView>();
            view.transform.SetParent(transform);
            return view.gameObject;
        }
    }

    public class Factory : PlaceholderFactory<IActionMenuData,ActionDataView>
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
