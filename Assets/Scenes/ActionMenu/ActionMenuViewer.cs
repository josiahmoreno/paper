using System;
using System.Collections;
using System.Collections.Generic;
using Heroes;
using MenuData;
using Tests;
using UnityEngine;
using Zenject;

public class ActionMenuViewer : MonoBehaviour
{
    public Battle.Battle Battle;
    public ActionMenuViewSettings ActionMenuViewSettings;
    private IActionMenu actionMenu;
    private float radius;
    private double preStartingAngle_ , endingAngle  = Math.PI / 6;
   
  
    [Inject]
    public void Construct(IActionViewerViewModel viewModel)
    {
        Debug.Log($"{GetType().Name} - Construct");
        viewModel.PropertyChanged += (sender, args) =>
        {
            switch (args.PropertyName)
            {
                case "ActionViews":
                    //this.gameObject.SetActive( this.gameObject);
                    DrawAll(viewModel.ActionViews);
                    break;
            
            }

            
        };
      
      
    }
   
    private void DrawAll(List<ActionView> actionViews)
    {
        // var pos =  angled(radius,endingAngle);
        // lineRenderer.SetPosition(0, transform.position);
        // lineRenderer.SetPosition(1, pos);
        Debug.Log($"GameMenu - ActiveAction = {actionMenu.ActiveAction.Name}, {actionMenu.SelectedIndex} {actionMenu.Items.Length}");
        Debug.Log(actionMenu.SelectedIndex);
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < actionMenu.Items.Length; i++)
        {
            CreateView(endingAngle + (preStartingAngle_* i), actionViews[i]);
        }
    }
   
    private void CreateView(double angle, ActionView actionView)
    {
        //Debug.Log($"{actionMenuData.Name} {angle}");
        // var view00 = new GameObject();
        var pos =  angled(radius,angle);
        actionView.transform.SetParent(this.transform,true);
        var rect = actionView.GetComponent<RectTransform>();
        pos.y -= rect.sizeDelta.y;
        rect.localPosition = pos;
    
    }
    
    private Vector3 angled(double radius, double angle)
    {
        return position(radius * Math.Cos(angle),radius * Math.Sin(angle));
    }

    private Vector3 position(double x, double y)
    {
        float newX = (float)x;
        float newY = (float) (y);
        return new Vector3(newX, newY, 0.0f);
    }
}

public class ActionViewerViewModel: ViewModelBase, IActionViewerViewModel
{
    private IActionMenu ActionMenu;
    private ITurnSystem TurnSystem;
    public ActionViewerViewModel(IActionMenu actionMenu)
    {
        ActionMenu.OnActiveActionChanged += ActionMenuOnOnActiveActionChanged;
        TurnSystem.OnActiveChanged += OnActiveChanged;
    }
    
    
    private List<ActionView> _actionViews;

    public List<ActionView> ActionViews
    {
        get { return _actionViews; }
        set { _actionViews = value;
            NotifyPropertyChanged("ActionViews");
        }
    }
    
    private void ActionMenuOnOnActiveActionChanged(object sender, IActionMenuData e)
    {
        ActionViews = new List<ActionView>();
    }
    
    private void OnActiveChanged(object obj)
    {
        if (obj is Hero hero)
        {
            Debug.Log($"GameMenu - OnActiveChanged {obj}");
            DrawAll();
        }
    }
}

public interface IActionViewerViewModel: IViewModelBase
{
    List<ActionView> ActionViews { get; }
}

public class ActionMenuViewSettings
{
   
}