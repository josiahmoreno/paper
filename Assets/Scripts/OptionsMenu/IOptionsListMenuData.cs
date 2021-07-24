using UnityEngine;

public interface IOptionsListMenuData
{
    MenuData.IActionMenuData MenuData { get; set; }
    RectTransform PrototypeCell { get; set; }
    string MenuText { get; set; }
    Sprite MenuSprite { get; set; }
}