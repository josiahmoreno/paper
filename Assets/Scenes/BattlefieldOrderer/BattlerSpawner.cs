using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
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

        public IEnumerable<BattlerView> SpawnEnemies(List<Enemy> enemies)
        {
            positioner.LoadEnemies(enemies);
            return enemies.Select(enemy =>
            {
                Sprite sprite = provider.GetEnemySprite(enemy);
                var battler = new Battler(sprite);
                var view =  _factory.Create(battler,null);
                var position = positioner.GetPosition(enemy);
         
                Debug.Log($"BattlerSpawner {enemy} {position}");
                view.transform.localPosition = position;
            
                return view;
            });
        }
        public BattlerView Spawn(Hero hero)
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
        Vector3 GetPosition(Enemy enemy);
        void LoadEnemies(List<Enemy> enemies);
    }

    public class BattlefieldPositioner2: IBattlefieldPositioner2
    {
        public Vector3 MarioPosition;
        public Vector3 PartnerPosition;
        public Vector3 DefaultBaddiePositon1;
        public Vector3 DefaultBaddiePositon2;
        public Vector3 DefaultBaddiePositon3;
        public Vector3 DefaultBaddiePositon4;
        public Vector3[] PoVector3s;
        private Dictionary<Enemy, Vector3> enemyPositions = new Dictionary<Enemy, Vector3>();
        public BattlefieldPositioner2(global::BattlefieldOrderer orderer)
        {
            MarioPosition = orderer.MarioPosition.transform.localPosition;
            PartnerPosition = orderer.PartnerPosition.transform.localPosition;
            
            DefaultBaddiePositon1 = orderer.DefaultBaddiePositon.transform.localPosition;
            DefaultBaddiePositon2 = orderer.DefaultBaddiePositon2.transform.localPosition;
            DefaultBaddiePositon3 = orderer.DefaultBaddiePositon3.transform.localPosition;
            DefaultBaddiePositon4 = orderer.DefaultBaddiePositon4.transform.localPosition;
            PoVector3s = new[]
                { DefaultBaddiePositon1, DefaultBaddiePositon2, DefaultBaddiePositon3, DefaultBaddiePositon4 };

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

        public Vector3 GetPosition(Enemy enemy)
        {
            return enemyPositions[enemy];
        }

        public void LoadEnemies(List<Enemy> enemies)
        {
            enemyPositions.Clear();
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];
                enemyPositions.Add(enemy,PoVector3s[i]);
            }
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

        public Sprite GetEnemySprite(Enemy enemy)
        {
            return _scriptableObject.GetSprite(enemy.Identifier);
         
        }

    
    }
    public interface ICharacterResourceProvider
    {
        Sprite GetSpriteForHero(Hero hero);
        Sprite GetEnemySprite(Enemy enemy);
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