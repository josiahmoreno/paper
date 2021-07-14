using System.Collections.Generic;
using Heroes;

namespace Scenes.BattlefieldOrderer
{
    public interface IBattlefieldOrdererModel
    {
        List<IBattlerView> GetBattlers();
        IMario GetMario();
    }
}