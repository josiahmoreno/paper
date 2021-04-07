using System.Collections;
using System.Collections.Generic;
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
