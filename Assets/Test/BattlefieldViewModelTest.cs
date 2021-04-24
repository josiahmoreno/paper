using System.Collections.Generic;
using Battle;
using Heroes;
using NUnit.Framework;
using Moq;
using UnityEngine;

namespace Test
{
    [TestFixture]
    public class BattlefieldViewModelTest
    {
        private IBattleFieldViewModel _battleFieldViewModel;
        private Battle.Battle _battle;

        [SetUp]
        public void setup()
        {
            
            _battle = Mock.Of<Battle.Battle>();
            _battle.Heroes = new List<Hero>() {new Mario(), new Goombario()};
            //foo.Property1 = 1;
            var entityProvider = new Mock<ICharacterEntityProvider>();
            entityProvider.Setup(e => e.GetPrefab(It.IsAny<Heroes.Heroes>())).Returns(new GameObject());
            entityProvider.Setup(e => e.GetBattlePosition(It.IsAny<Heroes.Heroes>())).Returns(new Vector3());
            _battleFieldViewModel = new BattlefieldViewModel(_battle,entityProvider.Object);
        }
        [Test]
        public void CreateCharacterEntityWhenBattleIsStarting()
        {
            _battle.Start();
            Assert.IsTrue(_battle.State == BattleState.STARTED);
            Assert.NotNull(_battleFieldViewModel.CharacterEntities);
            Assert.IsTrue( _battleFieldViewModel.CharacterEntities.Count == 2);
            Assert.NotNull(_battleFieldViewModel.CharacterEntities[0].prefab);
            Assert.NotNull(_battleFieldViewModel.CharacterEntities[0].battleLocation);
        }
    }
}