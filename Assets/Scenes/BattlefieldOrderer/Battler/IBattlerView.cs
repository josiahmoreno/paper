using Enemies;
using Heroes;
using UnityEngine;

namespace Scenes.BattlefieldOrderer
{
    public interface IBattlerView
    {
        void LoadSprite(Sprite getSprite);
        void Load(Hero hero);
        void Load(Enemy enemy);
    }
}