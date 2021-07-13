using System.Collections.Generic;
using System.Linq;
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
        }

        public BattlerSpawner Spawner { get; set; }

        public List<IBattlerView> GetBattlers()
        {
            
            return Battle.Heroes.Select(hero =>
            {
                
                return Spawner.Spawn(hero);
            }).ToList();
        }
    }
}