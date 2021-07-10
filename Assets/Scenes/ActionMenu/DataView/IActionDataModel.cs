using MenuData;

namespace Scenes.ActionMenu.DataView
{
    public interface IActionDataModel
    {
        ActionViewItem MenuData { get; }
        bool IsSelected { get; }

        object GetSprite();
    }
}