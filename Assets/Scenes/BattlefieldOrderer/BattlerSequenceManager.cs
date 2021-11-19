using Enemies;
using Heroes;
using PaperLib.Sequence;
using System;
using System.Collections.Generic;
using static TestSequncer;

namespace Scenes.BattlefieldOrderer
{
    internal class BattlerSequenceManager
    {

        private List<BattlerView> Views;
        public void Load(List<BattlerView> heroes)
        {
            Views = heroes;
        }

        public void excuteSequence(IDamageTarget damageTarget)
        {
            var heroBattler = FindHero(damageTarget.hero);
            var enemyBattler = FindEnemy(damageTarget.target);
            executeJump(heroBattler, enemyBattler, damageTarget);
        }

        private BattlerView FindEnemy(IEntity enemy)
        {
            return Views.Find(v => v.Enemy == enemy);
        }

        private BattlerView FindHero(IEntity hero)
        {
            return Views.Find(v => v.Hero == hero);
        }

        private void executeJump(BattlerView mario, BattlerView goomba, IDamageTarget damageTarget)
        {
            //executeJump(new UnitySequenceable(mario), new Position(goomba.transform.lol), damageTarget);
        }
        public void executeJump(ISequenceable mario, IPositionable goomba, IDamageTarget damageTarget)
        {
            
            var jumpSequence = new JumpSequence(new PaperLogger());
            jumpSequence.Execute(mario, goomba,damageTarget);
        }
    }
}