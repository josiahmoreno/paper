using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Heroes;
using UnityEngine;

namespace Scenes.BattlefieldOrderer
{
    public class BattlefieldOrdererModel : IBattlefieldOrdererModel
    {
        private Battle.Battle Battle;

        public BattlefieldOrdererModel(Battle.Battle battle, BattlerSpawner spawner)
        {
            this.Spawner = spawner;
            Battle = battle;
            Battle.TurnSystem.OnSwapped += (sender, args) =>
            {
                Debug.Log("BattlefieldOrdererModel swap");
                var secondPos = Partner.transform.localPosition;
                var firstPos = Mario.transform.localPosition;
                Partner.transform.DOLocalMove(firstPos, .1f);
                Mario.transform.DOLocalMove(secondPos, .1f);
            };
        }
        

        public BattlerSpawner Spawner { get; set; }

        public List<IBattlerView> GetBattlers()
        {
            
            var heroes =  Battle.Heroes.Select(hero =>
            {
                
                var view =  Spawner.Spawn(hero);
                if (hero is IMario)
                {
                    this.Mario = view;
                }

                if (hero is Goombario)
                {
                    this.Partner = view;
                }
                    return (IBattlerView)view;
            }).ToList();
            
            heroes.AddRange(Spawner.SpawnEnemies(Battle.Enemies));
            return heroes;
        }

        public BattlerView Partner { get; set; }

        public BattlerView Mario { get; set; }

        public IMario GetMario()
        {
            var mario =  Battle.Heroes.First(h => h is IMario) as IMario;
            return mario;
        }
    }
}