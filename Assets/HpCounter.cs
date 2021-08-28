using Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HpCounter : MonoBehaviour
{
    [SerializeField]
    Text healthText;
    [Inject]
    IHealth Health;
    // Start is called before the first frame update
    void Start()
    {
        Health.OnHealthChange += Health_OnHealthChange;
        healthText.text = Health.CurrentValue.ToString() + "/" + Health.Max.ToString();
    }

    private void Health_OnHealthChange(object sender, int e)
    {
        healthText.text = e.ToString() + "/" + Health.Max.ToString() ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
