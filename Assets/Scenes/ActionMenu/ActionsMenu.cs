using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Heroes;
using MenuData;
using Scenes.ActionMenu;
using Tests;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class ActionsMenu : MonoBehaviour, IActionsMenuView
{
    [SerializeField] public GameObject StartingPosition;
    [SerializeField] public float StartPositionYOffset;

    [SerializeField] public float StartingAngle;

    [SerializeField] public float RadiusLength;
    [SerializeField] public float AngleSpacing;

   
    public List<IActionViewItem> MenuData;
    public List<GameObject> _views;
    [Inject]
    private IActionsMenuPresenter presenter;
    [Inject] private ActionDataView.Spawner _viewSpawner;
    [Inject] private IActionMenu ActionMenu;
    public struct Degrees
    {
        public float angle;

        public Degrees(float angle)
        {
            this.angle = angle;
        }

        public float toRadians()
        {
            return (float)((Math.PI / 180) * angle);
        }
    }

    private float firstLocation = 0;
    public void Reload()
    {
        var position = StartingPosition.transform.localPosition;
        position.y = position.y + StartPositionYOffset;
        //ActionMenu.MoveTargetUp();
        if (_views is { Count: > 0 })
        {
            _views.ForEach(Destroy);
            _views.Clear();
            start = 0;
        }

        int selectedIndex = ActionMenu.SelectedIndex;
         if (MenuData.Count == 4)
         {
             //MenuData.Add(MenuData[0]);
             //MenuData = MenuData.GetRange(0, 2);
             //selectedIndex = 1;
         }
        // int menuDifference = 3 - MenuData.Count;
        // if (menuDifference < 0 )
        // {
     
        // }
       
        
        float starting = StartingAngle + (AngleSpacing * selectedIndex); 
        //Debug.Log($" selectedIndex = {ActionMenu.SelectedIndex} ");
        for (var index = 0; index < MenuData.Count; index++)
        {
            var data = MenuData[index];
            
            var angle = new Degrees(starting - (index * AngleSpacing) );
            if (index == 0)
            {
                firstLocation = angle.angle;
            }
            //Debug.Log($"{data.name} - angle =({angle.angle}) Starting ={starting} - {index * AngleSpacing} ");
            (float, float) delta = ((float)(RadiusLength * Math.Cos(angle.toRadians())), ((float)(RadiusLength * Math.Sin(angle.toRadians()))));
            var newLocalPosition = new Vector3(position.x + delta.Item1, position.y + delta.Item2, 0.0f);
            
            var view = _viewSpawner.InstantiateView(data,null, this.transform);
            view.transform.localPosition = newLocalPosition;
            Debug.DrawLine(position, newLocalPosition);
            var debug = StartingPosition.transform.position;
            debug.y = debug.y + StartPositionYOffset;

            Debug.DrawLine(debug, view.transform.position, Color.blue, 11f, false);
            view.name = $"Element {index}";
            _views.Add(view);
        }
    }

    private  int start = 0;
    public void MoveUp()
    {
        // The shortcuts way
        var position = StartingPosition.transform.localPosition;
        position.y = position.y + StartPositionYOffset;
        start++;
        for (var index = 0; index < _views.Count; index++)
        {
            var angle = new Degrees((firstLocation - ( (index + start)  * (AngleSpacing))) );
            Debug.Log($"firstLocation({firstLocation}0 - (index({index}) + start({start})) * angleSpacing({AngleSpacing}) = {angle.angle}");
            (float, float) delta = ((float)(RadiusLength * Math.Cos(angle.toRadians())), ((float)(RadiusLength * Math.Sin(angle.toRadians()))));
            var newLocalPosition = new Vector3(position.x + delta.Item1, position.y + delta.Item2, 0.0f);
            
            Debug.Log($" {newLocalPosition}");
            var view = _views[index];
            view.transform.DOLocalMove(newLocalPosition, .1f);
        }

        
    }
    
    public void MoveDown()
    {
        // The shortcuts way
        var position = StartingPosition.transform.localPosition;
        position.y = position.y + StartPositionYOffset;
        start--;
        for (var index = 0; index < _views.Count; index++)
        {
            ;
            //var angle = new Degrees((90 - ( (index + start)  * (AngleSpacing))) );
            var angle = new Degrees((firstLocation - ( (index + start)  * (AngleSpacing))) );
            Debug.Log($" {angle.angle}");
            (float, float) delta = ((float)(RadiusLength * Math.Cos(angle.toRadians())), ((float)(RadiusLength * Math.Sin(angle.toRadians()))));
            var newLocalPosition = new Vector3(position.x + delta.Item1, position.y + delta.Item2, 0.0f);
            
            Debug.Log($" {newLocalPosition}");
            var view = _views[index];
            view.transform.DOLocalMove(newLocalPosition, .1f);
        }
        
    }

    public void Load(List<IActionViewItem> data)
    {
        MenuData = data;
        Reload();
    }
    void Start()
    {
       
        presenter.OnStart();
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(bool showing)
    {
        this.gameObject.SetActive(showing);
    }
}

public interface IActionsDataViewSpawner
{
    GameObject InstantiateView(IActionViewItem data, GameObject dataActionItemPrefab, Transform transform);
}

public class ActionMenuModel : IActionsMenuModel
{
    [Inject] private readonly ActionMenuSettings Settings;
    private readonly IActionMenu actionMenu;
    private readonly ITurnSystem turnSystem;

    public ActionMenuModel(ITurnSystem turnSystem,IActionMenu actionMenu, IMenuMovementChangeProvider movementChangeProvider)
    {

        this.actionMenu = actionMenu;
        this.turnSystem = turnSystem;
       
        this.turnSystem.OnActiveChanged += o =>
        {
            if (o is Hero hero)
            {
                onCharacterChange?.Invoke( this,Fetch(hero,hero.Actions));
                //GetActionsData();
            }
        };
        movementChangeProvider.OnMoveUp += (sender,_) => OnMoveUp?.Invoke(this,null);
        movementChangeProvider.OnMoveDown += (sender,_) => OnMoveDown?.Invoke(this,null);
    }

    event EventHandler<bool> IActionsMenuModel.OnHide
    {
        add
        {
            this.actionMenu.OnHide += value;
        }

        remove
        {
            this.actionMenu.OnHide -= value;
        }
    }

    private List<IActionViewItem> Fetch(Hero hero,IActionMenuData[] data)
    {
        return data.Select(data =>
        {
            //Debug.Log($"ActionMenuModel - {data.Name}");
            var item = new ActionViewItem(data, Settings.ResourceProvider.GetSpriteForMenuData(hero,data), Settings.ActionViewPrefab) as IActionViewItem;

            return item;
        }).ToList();
    }
    public List<IActionViewItem> GetActionsData()
    {
        return Fetch(this.turnSystem.Active as Hero,this.actionMenu.Items);
    }

    public event EventHandler<List<IActionViewItem>> onCharacterChange;
    public event EventHandler OnMoveUp;

    public event EventHandler OnMoveDown;
}

public interface IMenuMovementChangeProvider
{
    public event EventHandler OnMoveUp;
    public event EventHandler OnMoveDown;
}

internal interface IActionsMenuPresenter
{
    void OnStart();
    public IActionsMenuView View { get; set; }

    
}


public interface IActionsMenuView
{
    void Load(List<IActionViewItem> getActionsData);
    void MoveUp();

    void MoveDown();
    void Show(bool showing);
}

[Serializable]
public class ActionViewItem: IActionViewItem 
{
    public string _name;
    
    public Sprite _sprite; 
    public IActionMenuData data { get; set; }
    public string name => _name;

    public Sprite sprite => _sprite;


    public ActionViewItem(IActionMenuData data, Sprite sprite, GameObject actionItemPrefab)
    {
        this.data = data;
        this._name = data?.Name;
        this._sprite = sprite;
        ActionItemPrefab = actionItemPrefab;
    }

    public GameObject ActionItemPrefab { get; private set; }
}

public interface IActionViewItem
{
    IActionMenuData data { get; set; }
    
    string name { get; }

    Sprite sprite { get; }
}
