using System;
using System.Collections.Generic;
using UnityEngine;

namespace OptionsMenu
{
    public interface IOptionsMenuModel
    {
        List<IOptionsListViewItem> GetOptionItems();
        RectTransform PrototypeCell { get; }
        string MenuText { get; }
        Sprite MenuSprite { get; }
        bool Showing { get; }
        event EventHandler<bool> ShowingChanged;
        event  EventHandler DataChanged;
    }
}