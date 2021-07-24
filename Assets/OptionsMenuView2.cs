using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using OptionsMenu;
using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsMenuView2 : MonoBehaviour, IOptionsMenu2View, IRecyclableScrollRectDataSource
{
    [Inject] public IOptionsMenuPresenter Presenter;
    [SerializeField] private RecyclableScrollRect ScrollRect;
    [SerializeField] private Text MenuText;
    private List<IOptionsListViewItem> Data;

    [SerializeField] public Image MenuIcon;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log($"{GetType().Name} 0 ");
        ScrollRect.DataSource = this;
     
        Debug.Log($"{GetType().Name} 1 ");
        
    }

    public void Start()
    {
        Presenter.OnStart();
    }

    // Update is called once per frame

    public void SetMenuText(string menuText)
    {
        MenuText.text = menuText;
    }

    public void SetMenuIcon(Sprite menuIcon)
    {
        MenuIcon.sprite = menuIcon;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void SetPrototypeCell(RectTransform prototypeCell)
    {
        Debug.Log($"{GetType().Name} - SetProtoTypeCell {prototypeCell} ");
        //ScrollRect.PrototypeCell = prototypeCell;
    }
    public void Load(List<IOptionsListViewItem> item)
    {
        Debug.Log($"{GetType().Name} - Load {item.Count} {string.Join(",",item)}");
        
        this.Data = item;
        ScrollRect.ReloadData();
    }

    

    public int GetItemCount()
    {
        return Data?.Count ?? 0;
    }

    public void SetCell(ICell cell, int index)
    {
        var itemCell = cell as IOptionsListItemCell;
        itemCell.Configure(Data[index]);
    }

    class Factory: PlaceholderFactory<OptionsMenuView2>
    {
        
    }
}

public interface IOptionsListItemCell : ICell
{
    void Configure(IOptionsListViewItem optionsListViewItem);
}

public interface IOptionsMenu2View
{
    void SetMenuText(string menuText);
    void Load(List<IOptionsListViewItem> item);
    void SetPrototypeCell(RectTransform prototypeCell);
    void SetMenuIcon(Sprite menuIcon);
    void Show();
    void Hide();

}

public interface IOptionsListViewItem
{
    string Name { get; }
    bool isSelected { get; }
    event EventHandler<bool> SelectedChange;
}
