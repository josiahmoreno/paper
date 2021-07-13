using System.Collections.Generic;

namespace Scenes.BattlefieldOrderer
{
    public interface IBattlefieldOrdererModel
    {
        List<IBattlerView> GetBattlers();
    }
}