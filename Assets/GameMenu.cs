using System;
using System.Collections;
using System.Collections.Generic;
using Heroes;
using MenuData;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameBattleProvider GameBattleProvider;
    public Battle.Battle Battle;
    private IActionMenu actionMenu;
    private float radius;
    private float height_;
    private double preStartingAngle_ , endingAngle  = Math.PI / 6;
    private LineRenderer lineRenderer;
    public int lengthOfLineRenderer = 20;
    void Start()
    {
        // this.lineRenderer = gameObject.AddComponent<LineRenderer>();
        // lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        // lineRenderer.widthMultiplier = 0.2f;
        // lineRenderer.positionCount = 2;
        
        Battle = GameBattleProvider.Battle;
        this.actionMenu = Battle.ActionMenu;
        this.actionMenu.OnHide += onHide;
        GameBattleProvider.Battle.ActionMenu.OnActiveActionChanged += ActionMenuOnOnActiveActionChanged;
        GameBattleProvider.Battle.TurnSystem.OnActiveChanged += OnActiveChanged;
        //this.Orientation = Orientation.Vertical;
        this.radius = this.GetComponent<RectTransform>().rect.height/2;
        this.height_ = this.GetComponent<RectTransform>().rect.height/2;
        this.preStartingAngle_ = endingAngle;
        //endingAngle;
            
        var diff = actionMenu.Items.Length - actionMenu.SelectedIndex - 1 ;
        this.endingAngle -= (diff * preStartingAngle_);
        System.Diagnostics.Debug.WriteLine($"ActionMenu - diff = {diff}");
        DrawAll();
        onHide(actionMenu.Showing);
        
    }

    private void ActionMenuOnOnActiveActionChanged(object sender, IActionMenuData e)
    {
        DrawAll();
    }

    private void OnActiveChanged(object obj)
    {
        if (obj is Hero hero)
        {
            Debug.Log($"GameMenu - OnActiveChanged {obj}");
            DrawAll();
        }
    }


    public void HandleAction(InputAction.CallbackContext context)
    {
        // 2
        
        if(context.action.activeControl.name == "downArrow" && !context.action.activeControl.IsPressed())
        {
            var old = Battle.ActionMenu.ActiveAction;
           
          Battle.MoveTargetDown();
          var newAction = Battle.ActionMenu.ActiveAction;
          if (old != newAction)
          {
              endingAngle += preStartingAngle_;
              
       
              DrawAll();
          }
          
        }
        
        if(context.action.activeControl.name == "upArrow" && !context.action.activeControl.IsPressed())
        {
            var old = Battle.ActionMenu.ActiveAction;
        
            Battle.MoveTargetUp();
            var newAction = Battle.ActionMenu.ActiveAction;
            if (old != newAction)
            {
                endingAngle -= preStartingAngle_;
                
       
                DrawAll();
            }
        }
        
        
            
        
    }

    private void OnDrawGizmos()
    {
        var pos =  angled(radius,endingAngle);
        Gizmos.DrawLine (transform.position, pos);
    }

    private void DrawAll()
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
            var item = actionMenu.Items[actionMenu.Items.Length - i - 1];
            UnityEngine.Color color = UnityEngine.Color.white;
            
            if (item == actionMenu.ActiveAction)
            {
                color = UnityEngine.Color.magenta;
            }
            // if (i == actionMenu.Items.Length - 1)
            // {
            //     color = Color.white;
            // }
            CreateView(endingAngle + (preStartingAngle_* i), item, color);
        }
    }
    
    private void CreateView(double angle, IActionMenuData actionMenuData, Color color)
    {
        //Debug.Log($"{actionMenuData.Name} {angle}");
       // var view00 = new GameObject();
        
        var view = new GameObject();
        view.name = "ActonView";
        Vector3 scale = view.transform.localScale;
        //view.transform.SetParent(view00.transform,false);
        var rect = view.AddComponent<RectTransform> ();
        view.transform.SetParent(this.transform,true);
        rect.sizeDelta = new Vector2(100,100);
        var pos =  angled(radius,angle);
        pos.y -= rect.sizeDelta.y;
        rect.pivot = new Vector2(.5f, .5f);
        rect.localPosition = pos;
        view.AddComponent<CanvasRenderer>();
        var image = view.AddComponent<Image>();
        image.color = color;
        var textView = new GameObject();
        textView.name = "Text";
        var labelRect = textView.AddComponent<RectTransform> ();
        labelRect.sizeDelta = new Vector2(100,100);
        var labelText = textView.AddComponent<Text>();
        labelText.font = Font.CreateDynamicFontFromOSFont("arial", 14);
        labelText.color = Color.gray;
        labelText.text = actionMenuData.Name;
        labelText.alignment = TextAnchor.MiddleCenter;
        textView.transform.SetParent(view.transform);
        
        //view00.transform.SetParent(this.transform);
        rect.transform.localScale = scale;
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
    private void onHide(bool obj)
    {
        this.gameObject.SetActive( obj);
    }
    
    public void OnMove(InputValue value)
    {
        //value.
        // Read value from control. The type depends on what type of controls.
        // the action is bound to.
        var v = value.Get<Vector2>();

        // IMPORTANT: The given InputValue is only valid for the duration of the callback.
        //            Storing the InputValue references somewhere and calling Get<T>()
        //            later does not work correctly.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}