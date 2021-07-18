using System.Collections;
using System.Collections.Generic;
using TargetSystem2;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TargetSystemView2 : MonoBehaviour, ITargetSystemView
{
    [Inject]
    private ITargetSystemPresenter presenter;

   

    public void ShowTargetInformation(TargetInformationArgs e)
    {
        //throw new System.NotImplementedException();
    }

    public void StopShowing()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
