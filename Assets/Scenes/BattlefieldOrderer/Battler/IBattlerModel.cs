using UnityEngine;

namespace Scenes.BattlefieldOrderer
{
    public interface IBattlerModel
    {
        Sprite GetSprite();
    }

    public class BattlerModel : IBattlerModel
    {
        private IBattler Battler;

        public BattlerModel(IBattler battler)
        {
            Battler = battler;
        }
        public Sprite GetSprite()
        {
            return Battler.Sprite;
        }
    }
}