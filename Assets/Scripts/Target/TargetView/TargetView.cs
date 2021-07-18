using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TargetView : MonoBehaviour, ITargetView
{
    [Inject]
    private ITargetViewPresenter Presenter;
    public Image targetIcon;

    private void Start()
    {
        Presenter.OnStart();
    }

    public void ShowTargeted()
    {
        targetIcon.enabled = true;
    }

    public void HideTargeted()
    {
        targetIcon.enabled = false;
    }

    internal class Factory : PlaceholderFactory<Enemy, TargetView>
    {
    }
    // Start is called before the first frame update

}
