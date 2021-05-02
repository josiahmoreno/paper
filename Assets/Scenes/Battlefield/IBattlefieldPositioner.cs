using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


public interface IBattlefieldPositioner
{
    void SetBattlePosition(IEnumerable<CharacterEntity> characterEntity);
    Vector3 GetPosition(Heroes.Heroes battleHeroIdentity, ObservableCollection<CharacterEntity> characterEntities);
    Transform GetDefaultPosition { get; }
}