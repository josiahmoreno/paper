using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionItemCell : MonoBehaviour, IOptionsListItemCell
{
    [SerializeField] private Text OptionName;
    [SerializeField] private Image SelectedIcon;
    private IOptionsListViewItem item;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        this.item.SelectedChange -= OnSelectedChange;
    }

    public void Configure(IOptionsListViewItem optionsListViewItem)
    {
      
        if (item != null) item.SelectedChange -= OnSelectedChange;
        
        OptionName.text = optionsListViewItem.Name;
        SelectedIcon.enabled = optionsListViewItem.isSelected;
        optionsListViewItem.SelectedChange += OnSelectedChange;
        this.item = optionsListViewItem;
        //throw new System.NotImplementedException();
    }
    
    

    private void OnSelectedChange(object sender, bool isSelected)
    {
        SelectedIcon.enabled = isSelected;
    }
}
