using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TesterTargetViewScript : MonoBehaviour
{
    [Inject]
    Battle.Battle Battle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Execute()
    {
        Battle.Execute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
