using System;
using UnityEngine;

namespace EntityProvider
{
    public class IEnityProviderImpl : MonoBehaviour, ICharacterEntityProvider
    {
        private GameObject Mario;
        private GameObject Goombario;
        public GameObject GetPrefab(Heroes.Heroes battleHeroIdentity)
        {
            var prefab = Resources.Load<GameObject>($"Heroes/{battleHeroIdentity}/{battleHeroIdentity}");
            if (prefab == null)
            {
                throw new NullReferenceException($"prefab for {battleHeroIdentity} not found");
            }
            return Instantiate(prefab);
        }

        public Vector3 GetBattlePosition(Heroes.Heroes isAny)
        {
            throw new System.NotImplementedException();
        }
    }
}