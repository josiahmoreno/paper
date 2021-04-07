using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounterView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameBattleProvider Provider;
    public Text MarioHealthText;
    void Start()
    {
        
        Provider.Battle.HealthCounter.OnShowing += b =>
        {
            Debug.Log($"{GetType().Name} - Visible = {b}");
            this.gameObject.SetActive(b);
        };
        this.gameObject.SetActive(Provider.Battle.HealthCounter.Showing);
        var mario = Provider.Battle.Heroes.First(h => h.Identity == Heroes.Heroes.Mario);
        MarioHealthText.text = $"{mario.Health.CurrentValue}";
        mario.Health.OnHealthChange += (sender, i) => MarioHealthText.text = $"{i}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
