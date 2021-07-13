using System;
using Heroes;
using UnityEngine;

namespace Scenes.BattlefieldOrderer
{
    [CreateAssetMenu(fileName = "CharacterSpritesScriptableObject", menuName = "Battlefield/CharacterSpritesScriptableObject", order = 0)]
    public class CharacterSpritesScriptableObject : ScriptableObject, ICharacterResourceProvider
    {
        public Sprite Mario;
        public Sprite Goombario;
        
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
    }
}