using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleProvider2 : MonoBehaviour, IBattleProvider
{
    public BattleLoader BattleLoader = new BattleLoader();
    public GameBattleScriptableObject GameBattleScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Battle.Battle Battle => BattleLoader.ConvertToBattle(GameBattleScriptableObject);
}

public interface IBattleProvider
{
    public Battle.Battle Battle { get; }
}
