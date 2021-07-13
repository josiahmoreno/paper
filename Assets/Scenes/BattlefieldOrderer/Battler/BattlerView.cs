using System;
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

        private void Start()
        {
            Presenter.OnStart();
        }
        
        public void LoadSprite(Sprite getSprite)
        {
            sprite.sprite = getSprite;
        }
    }
}