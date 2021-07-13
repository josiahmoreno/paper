using System;
using Heroes;
using UnityEngine;
using Zenject;

namespace Scenes.BattlefieldOrderer
{
    public class BattlerSpawner
    {
        public BattlerSpawner.Factory _factory;
        private ICharacterResourceProvider provider;
        private readonly IBattlefieldPositioner2 positioner;

        public BattlerSpawner(Factory factory, ICharacterResourceProvider provider, IBattlefieldPositioner2 positioner)
        {
            this.positioner = positioner;
            this.provider = provider;
            Debug.Log($"BattlerSpawner provider == null {provider}");
            _factory = factory;
        }

        public IBattlerView Spawn(Hero hero)
        {
            Debug.Log($"BattlerSpawner spawn provider == null {provider}");
            var sprite = provider.GetSpriteForHero(hero);
            var battler = new Battler(sprite);
            var view =  _factory.Create(battler,null);
            var position = positioner.GetPosition(hero);
         
                Debug.Log($"BattlerSpawner {hero} {position}");
                view.transform.localPosition = position;
            
            return view;
        }

        public class Factory : PlaceholderFactory<IBattler, Transform, BattlerView>
        {
            
        }
    }

    public interface IBattlefieldPositioner2
    {
        Vector3 GetPosition(Hero hero);
    }

    public class BattlefieldPositioner2: IBattlefieldPositioner2
    {
        public Vector3 MarioPosition;
        public Vector3 PartnerPosition;
        public Vector3 DefaultBaddiePositon;

        public BattlefieldPositioner2(global::BattlefieldOrderer orderer)
        {
            MarioPosition = orderer.MarioPosition.transform.localPosition;
            var localPosition = orderer.PartnerPosition.transform.localPosition;
            PartnerPosition = localPosition;
            DefaultBaddiePositon = localPosition;
            
        }
        public Vector3 GetPosition(Hero hero)
        {
            if (hero.Identity is Heroes.Heroes.Mario)
            {
                return MarioPosition;
            }

            if (hero.Identity is Heroes.Heroes.Goombario)
            {
                return PartnerPosition;
            }

            throw new Exception($"cant find position of {hero.Identity}");
        }
    }

    public class CharacterResourceProvider : ICharacterResourceProvider
    {
        CharacterSpritesScriptableObject _scriptableObject;
        public CharacterResourceProvider(CharacterSpritesScriptableObject scriptableObject)
        {
            this._scriptableObject = scriptableObject;
        }
        public Sprite GetSpriteForHero(Hero hero)
        {
            switch (hero.Identity)
            {
                case Heroes.Heroes.Mario:
                    return _scriptableObject.Mario;
                case Heroes.Heroes.Goombario:
                    return _scriptableObject.Goombario;
            }

            throw new Exception("cant find sprite");
        }
    }
    public interface ICharacterResourceProvider
    {
        Sprite GetSpriteForHero(Hero hero);
    }

    public class Battler: IBattler
    {
        public Sprite Sprite { get; }

        public Battler(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}