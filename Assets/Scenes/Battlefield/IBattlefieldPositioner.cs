using System.Collections.Generic;
using UnityEngine;
using Heroes = Heroes.Heroes;

public interface IBattlefieldPositioner
{
    void SetBattlePosition(IEnumerable<CharacterEntity> characterEntity);
}