using System.Collections.Generic;
using Heroes;
using Scenes.BattlefieldOrderer;

public interface IBattlefieldOrdererView
{
    void LoadCharacters(List<IBattlerView> battlers);
    void LoadMario(IMario mario);
}