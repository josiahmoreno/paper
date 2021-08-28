using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Heroes;
using Scenes.BattlefieldOrderer;
using UnityEngine;
using Zenject;

public class BattlefieldOrderer : MonoBehaviour, IBattlefieldOrdererView
{
    [Inject] public IBattlefieldOrdererPresenter Presenter;

    public GameObject MarioPosition;

    public GameObject PartnerPosition;

    public GameObject DefaultBaddiePositon;
    public GameObject DefaultBaddiePositon2;
    public GameObject DefaultBaddiePositon3;
    public GameObject DefaultBaddiePositon4;
    public List<BattlerView> BattlerViews;
    // Start is called before the first frame update
    void Start()
    {
        Presenter.OnStart();
    }
    

    public void LoadCharacters(List<IBattlerView> battlers)
    {
        Debug.Log($"LoadCharacters");
        for (int i = 0; i < battlers.Count; i++)
        {
            var battler = battlers[i];
            Debug.Log(battler);
            
        }
        BattlerViews = battlers.Cast<BattlerView>().ToList();
        
    }

    public void LoadMario(IMario mario)
    {
        
    }

    public void Swap(Transform first, Transform second)
    {
        var secondPos = second.localPosition;
        var firstPos = first.localPosition;
        first.transform.DOLocalMove(secondPos, .1f);
        second.transform.DOLocalMove(firstPos, .1f);
    }
}