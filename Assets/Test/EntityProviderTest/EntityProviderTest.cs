using EntityProvider;
using NUnit.Framework;
using UnityEngine;

namespace Test.EntityProviderTest
{
    [TestFixture]
    public class EntityProviderTest
    {
        [Test]
        public void GetMario()
        {
            ICharacterEntityProvider _enityProvider = new GameObject().AddComponent<IEnityProviderImpl>();
            Assert.NotNull(_enityProvider.GetGameObjectFromPrefab(Heroes.Heroes.Mario));
        }
    }

    
}