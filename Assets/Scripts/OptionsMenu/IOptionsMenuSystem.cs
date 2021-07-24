using System;
using MenuData;

namespace OptionsMenu
{
    public interface IOptionsMenuSystem
    {
        event EventHandler<IOptionsListMenuData> DataChanged;
        IOptionsListMenuData Data { get; }
        bool Showing { get; }
        event EventHandler<bool> OnShowing;
        event EventHandler<IOption> OnActiveOptionChanged;
        IOption ActiveOption { get; }
    }
}