using System;
using Enemies;
using Heroes;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes.BattlefieldOrderer
{
    public class BattlerView: MonoBehaviour, IBattlerView
    {
        [SerializeField]
        public Image sprite;

        [Inject] public IBattlerPresenter Presenter;

        public Hero Hero { get; private set; }
        public Enemy Enemy { get; private set; }

        private void Start()
        {
            Presenter.OnStart();
            
        }
        
        public void LoadSprite(Sprite getSprite)
        {
            sprite.sprite = getSprite;
            sprite.preserveAspect = true;
        }

        public void Load(Hero hero)
        {
            this.Hero = hero;
        }

        public void Load(Enemy enemy)
        {
            this.Enemy = enemy;
        }
    }
}