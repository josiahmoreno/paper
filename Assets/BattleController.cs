using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class BattleController : MonoBehaviour
{
    
    public GameBattleProvider Provider;
    [Inject]
    public Battle.Battle Battle;
    public PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        
        var sub = PlayerInput.actions["Submit"];
       
        sub.performed += OnSubmitPerformed;
        var navigate = PlayerInput.actions["Navigate"];
        navigate.performed += ActionOnperformed;
        var cancel = PlayerInput.actions["Cancel"];
                cancel.performed += OnCancel;
        var keyboard = navigate.controls;
        var left = navigate.controls.First(control => control.name.Contains("left"));
        
        //PlayerInput.uiInputModule.move.ToInputAction().performed += ActionOnperformed;
        // var left = keyboard.actionMap["Left"];
        // left.performed += LeftOnperformed;

    }

    private void OnCancel(InputAction.CallbackContext obj)
    {
        var control = obj.control;
        if (control.IsPressed())
        {
            Battle.Cancel();
        }
    }

    public void HandleAction(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.name);
        var control = context.control;
        if (control.name.Contains("down") )
        {
            if (!control.IsPressed())
            {
                Debug.Log($"{control.name} pressed" );
                Battle.MoveTargetDown();
            }
        }
        if (control.name.Contains("up") )
        {
            if (!control.IsPressed())
            {
                Debug.Log($"{control.name} pressed" );
                Battle.MoveTargetUp();
            }
        }
    }

    private void ActionOnperformed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.action.name);
        var control = obj.control;
        if (control.name.Contains("left") )
        {
            if (control.IsPressed())
            {
                Debug.Log($"{control.name} pressed" );
                Battle.TargetSystem.MoveTargetLeft();
            }
        }
        if (control.name.Contains("right") )
        {
            if (control.IsPressed())
            {
                Debug.Log($"{control.name} pressed" );
                Battle.TargetSystem.MoveTargetRight();
            }
        }
    }

    private void LeftOnperformed(InputAction.CallbackContext obj)
    {
        Battle.TargetSystem.MoveTargetLeft();
    }

    private void OnSubmitPerformed(InputAction.CallbackContext obj)
    {
      Battle.Execute();
        
    }

    public void OnSwap(InputAction.CallbackContext obj)
    {
        
        if (obj.canceled)
        {
            Debug.Log($"{GetType().Name} - OnSwap ${obj.action.name} {obj.control.name}\n started ={obj.started} performed = {obj.performed} canceled = {obj.canceled}");
            Battle.TurnSystem.Swap();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
