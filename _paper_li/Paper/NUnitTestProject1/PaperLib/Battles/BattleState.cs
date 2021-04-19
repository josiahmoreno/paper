using System;

namespace Battle
{
    internal class BattleStateStore
    {
        public BattleState State = BattleState.NONE;
        internal bool IsStarted()
        {
            return State == BattleState.STARTED;
        }

        internal bool IsEnded()
        {
            return State == BattleState.ENDED;
        }
    }

    public enum BattleState { NONE,STARTING, STARTED, ENDING, ENDED }
}