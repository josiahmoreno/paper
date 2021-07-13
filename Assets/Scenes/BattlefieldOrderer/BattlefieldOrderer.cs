using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scenes.BattlefieldOrderer;
using UnityEngine;
using Zenject;

public class BattlefieldOrderer : MonoBehaviour, IBattlefieldOrdererView
{
    [Inject] public IBattlefieldOrdererPresenter Presenter;

    public GameObject MarioPosition;

    public GameObject PartnerPosition;

    public GameObject DefaultBaddiePositon;
    // Start is called before the first frame update
    void Start()
    {
        Presenter.OnStart();
    }
    

    public void LoadCharacters(List<IBattlerView> battlers)
    {
        for (int i = 0; i < battlers.Count; i++)
        {
            var battler = battlers[i];
            
        }
        
    }
}