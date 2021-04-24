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
using Heroes = Heroes.Heroes;


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

    [Inject] public Battle.Battle battle;
    public BattlefieldViewModel(Battle.Battle battle, ICharacterEntityProvider entityProvider, IBattlefieldPositioner battlefieldPositioner = null)
    {
        if (battle == null)
        {
            throw new NullReferenceException();
        }

        if (entityProvider == null)
        {
            throw new NullReferenceException();
        }

         
        
        Debug.Log($"{GetType().Name} -Construct");
        this._battle = battle;

        void OnBattleStateStoreOnBattleStateChanged(object sender, BatleStateChangeEventArgs args)
        {
            switch (args.BusinessObject)
            {
                case BattleState.NONE:
                    break;
                case BattleState.STARTING:
                    CharacterEntities = CreateCharacterEntities(_battle, entityProvider);
                    
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
        }

        this._battle.BattleStateStore.BattleStateChanged += OnBattleStateStoreOnBattleStateChanged;
        OnBattleStateStoreOnBattleStateChanged(_battle,new BatleStateChangeEventArgs(_battle.State));
    }

    private ObservableCollection<CharacterEnity> CreateCharacterEntities(Battle.Battle battle, ICharacterEntityProvider entityProvider)
    {
        ObservableCollection<CharacterEnity> characterEnities = new ObservableCollection<CharacterEnity>();
        foreach (var battleHero in battle.Heroes)
        {
            
            GameObject prefab = entityProvider.GetPrefab(battleHero.Identity ?? throw new Exception());
             characterEnities.Add( new CharacterEnity(battleHero,prefab));
        }

        return characterEnities;
    }
}

public interface IBattlefieldPositioner
{
    
}

public interface ICharacterEntityProvider
{
    GameObject GetPrefab(global::Heroes.Heroes battleHeroIdentity);
    Vector3 GetBattlePosition(global::Heroes.Heroes isAny);
}

public class CharacterEnity
{
    public GameObject prefab;
    public Vector3 battleLocation;
    
    public CharacterEnity(Hero battleHero, GameObject prefab)
    {
        this.prefab = prefab;
    }
}


