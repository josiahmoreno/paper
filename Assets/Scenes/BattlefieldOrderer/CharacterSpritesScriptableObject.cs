using System;
using Enemies;
using Heroes;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Scenes.BattlefieldOrderer
{
    [CreateAssetMenu(fileName = "CharacterSpritesScriptableObject", menuName = "Battlefield/CharacterSpritesScriptableObject", order = 0)]
    public class CharacterSpritesScriptableObject : ScriptableObject, ICharacterResourceProvider
    {
        public Sprite Mario;
        public Sprite Goombario;
        public List<SpriteObject> Sprites;
        public Sprite GetSpriteForHero(Hero hero)
        {
            switch (hero.Identity)
            {
                case Heroes.Heroes.Mario:
                    return Mario;
                case Heroes.Heroes.Goombario:
                    return Goombario;
            }

            throw new Exception("cant find sprite");
        }

        public Sprite GetEnemySprite(Enemy enemy)
        {
            return GetSprite(enemy.Identifier);
        }

        public Sprite GetSprite(string enemyIdentifier)
        {
            return Sprites.First( s => s.Identifier == enemyIdentifier).Sprite;
        }
    }
    [Serializable]
    public struct SpriteObject
    {
        public string Identifier;
        public Sprite Sprite;
    }
}