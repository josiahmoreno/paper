using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Zenject;

namespace Scenes.Battlefield
{
    public class IBattlefieldPositionerImpl : MonoBehaviour,IBattlefieldPositioner
    {
        [SerializeField]
        public GameObject DefaultPosition;

        [SerializeField]
        public Animator Animator;
        [Inject]
        public Settings settings;
        public void SetBattlePosition(IEnumerable<CharacterEntity> characterEntity)
        {
            Debug.Log($"{GetType().Name} SetBattlePosition");
            var position = new Vector3(0,0,0);
            foreach (var entity in characterEntity)
            {
                if(entity.prefab.TryGetComponent<Animator>(out Animator animatorr))
                {
                    Animator = animatorr;
                    animatorr.enabled = false;
                } 
                var rect = entity.prefab.GetComponent<RectTransform>();
                entity.prefab.transform.parent = DefaultPosition.transform;
                //Debug.Log($"{GetType().Name} Setting {entity.battleHero} at {position}");
                rect.localPosition = position;
                //ect.position = position;
                rect.localScale = Vector3.one;
               
                rect.pivot = new Vector2(.5f, 0f);
                Debug.Log($"{GetType().Name}  {entity.battleHero} at {rect.position}");
                position = new Vector3((position.x - ((RectTransform)entity.prefab.transform).rect.width),0, position.z + settings.ZDepthStep);
                var animator = entity.prefab.GetComponent<Animator>();
                if (animator == null)
                {
                    var temp = entity.prefab.AddComponent<Animator>();
                    temp.runtimeAnimatorController = Animator.runtimeAnimatorController;
                    temp.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                }
                else
                {
                    animator.enabled = true;
                    animator.StopPlayback();
                }
                

            }
            
            //throw new System.NotImplementedException();
        }

        public Vector3 GetPosition(Heroes.Heroes battleHeroIdentity,
            ObservableCollection<CharacterEntity> characterEntities)
        {
            Debug.Log($"{GetType().Name} SetBattlePosition");
            var position = new Vector3(0, 0, 0);
            bool found = false;
            for (int i = 0; i < characterEntities.Count; i++)
            {
                
                if (battleHeroIdentity == characterEntities[i].battleHero.Identity)
                {
                    found = true;
                    return position;
                }

                position = new Vector3((position.x - i*58), 0,
                    position.z + settings.ZDepthStep);
              
            }

            return position;
        }
    

        public Transform GetDefaultPosition
        {
            get => DefaultPosition.transform;
        }

        [Serializable]
        public class Settings
        {
            public float ZDepthStep;
        }
    }
}