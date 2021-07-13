using System.Collections.Generic;
using Scenes.BattlefieldOrderer;

public interface IBattlefieldOrdererView
{
    void LoadCharacters(List<IBattlerView> battlers);
}