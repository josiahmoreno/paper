using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Battle;
using UnityEngine;
using Zenject;

namespace EntityProvider
{
    public class IEnityProviderImpl : MonoBehaviour, ICharacterEntityProvider
    {
        public ObservableCollection<CharacterEntity> CharacterEntities { get; set; }
        public event EventHandler<List<CharacterEntity>> OnCharactersSpawned;
        private GameObject Mario;
        private GameObject Goombario;
        private Battle.Battle Battle;
        private  IBattlefieldPositioner _battlefieldPositioner;
        [Inject]
        public void Construct(Battle.Battle battle, IBattlefieldPositioner battlefieldPositioner)
        {
            Debug.Log($"{GetType().Name} init");
            this.Battle = battle;
            this._battlefieldPositioner = battlefieldPositioner;
            init();
        }
        

        private void init()
        {
            Debug.Log($"{GetType().Name} init");
            void OnBattleStateStoreOnBattleStateChanged(object sender, BatleStateChangeEventArgs args)
            {
                switch (args.BusinessObject)
                {
                    case BattleState.NONE:
                        break;
                    case BattleState.STARTING:
                        this.CharacterEntities = CreateCharacterEntities(Battle);
                        this._battlefieldPositioner.SetBattlePosition(CharacterEntities);
                                
                                
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

        

        private ObservableCollection<CharacterEntity> CreateCharacterEntities(Battle.Battle battle)
        {
            ObservableCollection<CharacterEntity> characterEnities = new ObservableCollection<CharacterEntity>();
            foreach (var battleHero in battle.Heroes)
            {
            
                GameObject prefab = GetGameObjectFromPrefab(battleHero.Identity ?? throw new Exception());
                characterEnities.Add( new CharacterEntity(battleHero,prefab));
            }

            return characterEnities;
        }
        public GameObject GetGameObjectFromPrefab(Heroes.Heroes battleHeroIdentity)
        {
            var prefab = Resources.Load<GameObject>($"Heroes/{battleHeroIdentity}/{battleHeroIdentity}");
            if (prefab == null)
            {
                throw new NullReferenceException($"prefab for {battleHeroIdentity} not found");
            }

            return Instantiate(prefab);
            //return Instantiate(prefab,_battlefieldPositioner.GetPosition(battleHeroIdentity,CharacterEntities),new Quaternion(),_battlefieldPositioner.GetDefaultPosition);
        }
    }
}