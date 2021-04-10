using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    
    public GameBattleProvider Provider;

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
            Provider.Battle.Cancel();
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
                Provider.Battle.MoveTargetDown();
            }
        }
        if (control.name.Contains("up") )
        {
            if (!control.IsPressed())
            {
                Debug.Log($"{control.name} pressed" );
                Provider.Battle.MoveTargetUp();
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
                Provider.Battle.TargetSystem.MoveTargetLeft();
            }
        }
        if (control.name.Contains("right") )
        {
            if (control.IsPressed())
            {
                Debug.Log($"{control.name} pressed" );
                Provider.Battle.TargetSystem.MoveTargetRight();
            }
        }
    }

    private void LeftOnperformed(InputAction.CallbackContext obj)
    {
        Provider.Battle.TargetSystem.MoveTargetLeft();
    }

    private void OnSubmitPerformed(InputAction.CallbackContext obj)
    {
      Provider.Battle.Execute();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
