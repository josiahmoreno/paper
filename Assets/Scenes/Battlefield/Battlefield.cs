using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Battle;
using Heroes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Internal;


public class Battlefield : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Text Text;
    [Inject]
    public void Construct(IBattleFieldViewModel viewModel)
    {
        Debug.Log($"{GetType().Name} -Construct");
        viewModel.PropertyChanged += (sender, args) =>
        {
            switch (args.PropertyName)
            {
                case "CharacterEntities":
                    foreach (var viewModelCharacterEntity in viewModel.CharacterEntities)
                    {
                        var gameObject = Instantiate(viewModelCharacterEntity.prefab);
                    }
                    break;
            }

            
        };
    }

    
}

public interface IBattleFieldViewModel: INotifyPropertyChanged
{
    Battle.Battle _battle { get; }
    ObservableCollection<CharacterEnity> CharacterEntities { get; }
}
public class BattlefieldViewModel: IBattleFieldViewModel
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(string propertyName = "", [CallerMemberName] string callerName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public Battle.Battle _battle { get; private set; }

    private ObservableCollection<CharacterEnity> _characterEntities;

    public ObservableCollection<CharacterEnity> CharacterEntities
    {
        get { return _characterEntities; }
        set { _characterEntities = value;
            NotifyPropertyChanged("CharacterEntities");
        }
    }
    
    [Inject]
    public void Construct(Battle.Battle battle)
    {
        Debug.Log($"{GetType().Name} -Construct");
        this._battle = battle;
        this._battle.BattleStateStore.BattleStateChanged += (sender, args) =>
        {
            switch (args.BusinessObject)
            {
                case BattleState.NONE:
                    break;
                case BattleState.STARTING:
                    CharacterEntities = CreateCharacterEntities(_battle);
                    break;
                case BattleState.STARTED:
                    break;
                case BattleState.ENDING:
                    break;
                case BattleState.ENDED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };

    }

    private ObservableCollection<CharacterEnity> CreateCharacterEntities(Battle.Battle battle)
    {
        ObservableCollection<CharacterEnity> characterEnities = new ObservableCollection<CharacterEnity>();
        foreach (var battleHero in battle.Heroes)
        {
             characterEnities.Add( new CharacterEnity(battleHero));
        }

        return characterEnities;
    }
}

public class CharacterEnity
{
    public GameObject prefab;
    public Transform battleLocation;

    public CharacterEnity(Hero battleHero)
    {
        
    }
}


