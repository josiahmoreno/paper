using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Heroes;
using PaperLib.Sequence;
using UnityEngine;
using static TestSequncer;

namespace Scenes.BattlefieldOrderer
{
    public class BattlefieldOrdererModel : IBattlefieldOrdererModel
    {
        private Battle.Battle Battle;
        private BattlerSequenceManager battlerSequnceManager;
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

                //executeJump(Partner,Mario,null);
            };
        }

        //private void executeJump(BattlerView mario, BattlerView goomba, IDamageTarget damageTarget)
        //{
        //    executeJump(new UnitySequenceable(mario), new UnityMovementTarget(goomba.transform),damageTarget);
        //}
        //public void executeJump(ISequenceable mario, IMovementTarget goomba, IDamageTarget damageTarget)
        //{
        //    var jumpSequence = new JumpSequence(new PaperLogger(), damageTarget);
        //    jumpSequence.Execute(mario, goomba);
        //}


        public BattlerSpawner Spawner { get; set; }

        public List<IBattlerView> GetBattlers()
        {
            
            var heroes =  Battle.Heroes.Select(hero =>
            {
                
                var view =  Spawner.Spawn(hero);
                if (hero is Mario marioHero)
                {
                    this.Mario = view;
                    marioHero.Sequenceable = new UnitySequenceable(view);
                }

                if (hero is Goombario goombario)
                {
                    this.Partner = view;
                    goombario.Sequenceable = new UnitySequenceable(view);
                }
                    return (IBattlerView)view;
            }).ToList();
           
            heroes.AddRange(Spawner.SpawnEnemies(Battle.Enemies));
            this.Battlers = heroes;
            //battlerSequnceManager.Load(heroes.Cast<BattlerView>().ToList());
            return heroes;

        }

        public BattlerView Partner { get; set; }

        public BattlerView Mario { get; set; }
        public List<IBattlerView> Battlers { get; private set; }

        public IMario GetMario()
        {
            var mario =  Battle.Heroes.First(h => h is IMario) as IMario;
            return mario;
        }
    }
}