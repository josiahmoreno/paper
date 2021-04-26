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

public class BattlefieldViewModel: IBattleFieldViewModel
{
    [Inject]
    public Battle.Battle Battle { get; private set; }

    private ObservableCollection<CharacterEntity> _characterEntities;

    public ObservableCollection<CharacterEntity> CharacterEntities
    {
        get { return _characterEntities; }
        set { _characterEntities = value;
            NotifyPropertyChanged("CharacterEntities");
        }
    }
    public BattlefieldViewModel(Battle.Battle battle, ICharacterEntityProvider entityProvider, IBattlefieldPositioner battlefieldPositioner)
    {
        if (battle == null)
        {
            throw new NullReferenceException();
        }

        if (entityProvider == null)
        {
            throw new NullReferenceException();
        }

        if (battlefieldPositioner == null)
        {
            throw new NullReferenceException();
        }
        
        Debug.Log($"{GetType().Name} -Construct");
        this.Battle = battle;

        void OnBattleStateStoreOnBattleStateChanged(object sender, BatleStateChangeEventArgs args)
        {
            switch (args.BusinessObject)
            {
                case BattleState.NONE:
                    break;
                case BattleState.STARTING:
                    CharacterEntities = CreateCharacterEntities(Battle, entityProvider);
  
                    battlefieldPositioner.SetBattlePosition(CharacterEntities);
                    
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

        this.Battle.BattleStateStore.BattleStateChanged += OnBattleStateStoreOnBattleStateChanged;
        OnBattleStateStoreOnBattleStateChanged(Battle,new BatleStateChangeEventArgs(Battle.State));
    }

    private ObservableCollection<CharacterEntity> CreateCharacterEntities(Battle.Battle battle, ICharacterEntityProvider entityProvider)
    {
        ObservableCollection<CharacterEntity> characterEnities = new ObservableCollection<CharacterEntity>();
        foreach (var battleHero in battle.Heroes)
        {
            
            GameObject prefab = entityProvider.GetGameObjectFromPrefab(battleHero.Identity ?? throw new Exception());
             characterEnities.Add( new CharacterEntity(battleHero,prefab));
        }

        return characterEnities;
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(string propertyName = "", [CallerMemberName] string callerName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public interface ICharacterEntityProvider
{
    GameObject GetGameObjectFromPrefab(global::Heroes.Heroes battleHeroIdentity);
   
}

public class CharacterEntity
{
    public GameObject prefab;
    public Vector3 battleLocation;
    
    public CharacterEntity(Hero battleHero, GameObject prefab)
    {
        this.prefab = prefab;
    }
}


