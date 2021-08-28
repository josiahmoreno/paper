using PaperLib.Sequence;
using System;

internal class UnityQuicktime : IQuicktime
{
    public Func<bool> Getter => () =>
    {
        return true;
    };
}