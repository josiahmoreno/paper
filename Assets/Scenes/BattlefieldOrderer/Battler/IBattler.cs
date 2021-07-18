using Enemies;
using Heroes;
using UnityEngine;

namespace Scenes.BattlefieldOrderer
{
    public interface IBattler
    {
        Sprite Sprite { get; }

        Enemy Enemy { get; }

        Hero Hero { get; }
    }
}