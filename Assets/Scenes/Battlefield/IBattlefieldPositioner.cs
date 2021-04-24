using System.Collections.Generic;
using UnityEngine;
using Heroes = Heroes.Heroes;

public interface IBattlefieldPositioner
{
    Vector3 SetBattlePosition(IEnumerable<CharacterEntity> characterEntity);
}