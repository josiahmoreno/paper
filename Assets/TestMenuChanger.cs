using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestMenuChanger : MonoBehaviour, IMenuMovementChangeProvider
{
    [Inject] public IBattleProvider _provider;
    [SerializeField] private Button up;

    [SerializeField] private Button down;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    // Start is called before the first frame update
    private int index;
    void Start()
    {
        Debug.Log($"TestMenuChanger Start");
        index = _provider.Battle.ActionMenu.SelectedIndex;
        _provider.Battle.ActionMenu.OnActiveActionChanged += (sender, data) =>
        {
            var diff = index -
                       _provider.Battle.ActionMenu.SelectedIndex;
            index = _provider.Battle.ActionMenu.SelectedIndex;
            Debug.Log($"TestMenuChanger OnMenuDiff {diff}");
            if (diff == 1)
            {
                OnMoveUp?.Invoke(this,new EventArgs());
            } else if (diff == -1)
            {
                OnMoveDown?.Invoke(this,new EventArgs());
                //moveDown();
            }
        };
    }

    // Update is called once per frame
    public void moveUp()
    {
        _provider.Battle.MoveTargetUp();
        //OnMoveUp?.Invoke(this,new EventArgs());
    }
    
    public void moveLeft()
    {
        _provider.Battle.TargetSystem.MoveTargetLeft();
    }

    public void moveRight()
    {
        _provider.Battle.TargetSystem.MoveTargetRight();
    }

    public void execute()
    {
        _provider.Battle.Execute();
    }
    public void moveDown()
    {
        _provider.Battle.MoveTargetDown();
        
        //OnMoveDown?.Invoke(this,new EventArgs());
    }

    public void swap()
    {
        _provider.Battle.TurnSystem.Swap();
    }

    public event EventHandler OnMoveUp;
    public event EventHandler OnMoveDown;
}
