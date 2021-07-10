using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MenuData;
using Scenes.ActionMenu;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class ActionsMenu : MonoBehaviour, IActionsMenuView
{
    //[SerializeField] public GameObject ActionItemPrefab;

    [SerializeField] public GameObject StartingPosition;
    [SerializeField] public float StartPositionYOffset;
    
    [SerializeField] public float StartingAngle;

    [SerializeField] public float RadiusLength;
    // Start is called before the first frame update
    [SerializeField] public float AngleSpacing;

    //[SerializeField] public GameObject GameBattleProvider;
    public List<ActionViewItem> MenuData;
    public List<GameObject> _views;
    [Inject]
    private IActionsMenuPresenter presenter;

    [Inject] private IFactory<IActionMenuData,ActionDataView> _factory;
    //[Inject] private IActionsDataViewSpawner _viewSpawner;
    public struct Degrees
    {
        private float angle;

        public Degrees(float angle)
        {
            this.angle = angle;
        }

        public float toRadians()
        {
            return (float) ((Math.PI / 180) * angle);
        }
    }
    public void Reload()
    {
        var position = StartingPosition.transform.localPosition;
        position.y = position.y + StartPositionYOffset;
        if (_views is { Count: > 0 })
        {
            _views.ForEach(Destroy);
        }
        for (var index = 0; index < MenuData.Count; index++)
        {
            var data = MenuData[index];
            var angle = new Degrees((90 - (index * (AngleSpacing))) - StartingAngle);
            (float,float) delta = ((float)(RadiusLength * Math.Cos(angle.toRadians())), ((float) (RadiusLength * Math.Sin(angle.toRadians()))));
            var newLocalPosition = new Vector3(position.x + delta.Item1, position.y + delta.Item2, 0.0f);
            var view = _factory.Create(data.data).gameObject;
            //var view = _viewSpawner.InstantiateView(data.data,data.ActionItemPrefab, this.transform);
            //var view = Instantiate(data.ActionItemPrefab, this.transform);
            view.transform.localPosition = newLocalPosition;
            Debug.DrawLine(position,newLocalPosition);
            var debug = StartingPosition.transform.position;
            debug.y = debug.y + StartPositionYOffset;
            
            Debug.DrawLine(debug,view.transform.position,Color.blue,11f,false);
            view.name = $"Element {index}";
            _views.Add(view);
        }
    }

    public void Load(List<ActionViewItem> data)
    {
        MenuData = data;
        Reload();
    }
    void Start()
    {
        //var provider = GameBattleProvider.GetComponent<IBattleProvider>();
        //presenter = new ActionsMenuPresenter(new ActionMenuModel(provider));
        presenter.View = this;
        presenter.OnStart();
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface IActionsDataViewSpawner 
{
    GameObject InstantiateView(IActionMenuData data,GameObject dataActionItemPrefab, Transform transform);
}

public class ActionMenuModel: IActionsMenuModel
{
    [Inject] private readonly ActionMenuSettings Settings;
    [Inject]
    private readonly IBattleProvider provider;

    public ActionMenuModel(IBattleProvider provider)
    {
        this.provider = provider;
    }

    public List<ActionViewItem> GetActionsData()
    {
        return provider.Battle.ActionMenu.Items.Select(data =>
        {
            var item = new ActionViewItem(data,Settings.ResourceProvider.GetSpriteForMenuData(data), Settings.ActionViewPrefab);
            
            return item;
        }).ToList();
    }

    public event EventHandler<List<ActionViewItem>> onCharacterChange;
}

internal interface IActionsMenuPresenter
{
    void OnStart();
    public IActionsMenuView View { get; set; }
}


public interface IActionsMenuView
{
    void Load(List<ActionViewItem> getActionsData);
}

[Serializable]
public class ActionViewItem
{
    public string name;
    public Sprite sprite;
    public readonly IActionMenuData data;

    public ActionViewItem(IActionMenuData data, Sprite sprite, GameObject actionItemPrefab)
    {
        this.data = data;
        this.sprite = sprite;
        ActionItemPrefab = actionItemPrefab;
    }

    public GameObject ActionItemPrefab { get; private set; }
}
