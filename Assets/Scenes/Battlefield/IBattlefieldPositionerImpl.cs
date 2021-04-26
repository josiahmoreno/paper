using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Battlefield
{
    public class IBattlefieldPositionerImpl : MonoBehaviour,IBattlefieldPositioner
    {
        [SerializeField]
        public GameObject DefaultPosition;
        public void SetBattlePosition(IEnumerable<CharacterEntity> characterEntity)
        {
            var position = new Vector3(0,0,0);
            foreach (var entity in characterEntity)
            {
                var rect = entity.prefab.GetComponent<RectTransform>();
                entity.prefab.transform.parent = DefaultPosition.transform;
                rect.localPosition = position;
                rect.localScale = Vector3.one;
                rect.pivot = new Vector2(.5f, 0f);
                position = new Vector3((position.x - ((RectTransform)entity.prefab.transform).rect.width),0,0);
            }
            
            //throw new System.NotImplementedException();
        }
    }
}