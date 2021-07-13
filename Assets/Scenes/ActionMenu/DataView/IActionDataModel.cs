using System;
using System.ComponentModel;
using MenuData;
using UnityEngine.UIElements;

namespace Scenes.ActionMenu.DataView
{
    public interface IActionDataModel: INotifyPropertyChanged
    {
        IActionViewItem MenuData { get; }
        bool IsSelected { get; }

        event EventHandler<bool> OnSelectedChanged;

    }
}