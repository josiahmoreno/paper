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
                case "Swapping":
                    
                    break;
            }

            
        };
    }

    
}

public class BattlefieldViewModel: ViewModelBase,IBattleFieldViewModel
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

    private bool _swapping;

    public bool Swapping
    {
        get => _swapping;
        set
        {
            _swapping = value;
            NotifyPropertyChanged(nameof(Swapping));
        }
    }

    public class SwapTargets
    {
        public CharacterEntity partner;
        public CharacterEntity mario;
        public Animation animation;

        public SwapTargets(CharacterEntity partner, CharacterEntity mario, Animation animation)
        {
            this.partner = partner;
            this.mario = mario;
            this.animation = animation;
        }
    }
    public BattlefieldViewModel(Battle.Battle battle, ICharacterEntityProvider entityProvider )
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
        

    }

    
    
    
}

public class ViewModelBase: IViewModelBase
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(string propertyName = "", [CallerMemberName] string callerName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public interface ICharacterEntityProvider
{
    GameObject GetGameObjectFromPrefab(global::Heroes.Heroes battleHeroIdentity);
    ObservableCollection<CharacterEntity> CharacterEntities { get; }
    event EventHandler<List<CharacterEntity>> OnCharactersSpawned;
}

public class CharacterEntity
{
    public GameObject prefab;
    public Vector3 battleLocation;
    public readonly Hero battleHero;

    public CharacterEntity(Hero battleHero, GameObject prefab)
    {
        this.battleHero = battleHero;
        this.prefab = prefab;
    }
}