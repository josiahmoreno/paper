using UnityEngine;

namespace Scenes.BattlefieldOrderer
{
    public interface IBattlerModel
    {
        Sprite GetSprite();

        IBattler Battler { get; }

    }

    public class BattlerModel : IBattlerModel
    {
        public IBattler Battler { get;}
        
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