using System;
using MenuData;
using UnityEngine;
using Zenject;


    public class ResourceProvider : IResourceProvider
    {
        [Inject]
        public ActionMenuResourcesScriptableObject ActionMenuResourcesScriptableObject;
        public Sprite GetSpriteForMenuData(IActionMenuData data)
        {
            switch (data.Name)
            {
                case "Jump":
                    return ActionMenuResourcesScriptableObject.Jump;
                default:
                    throw new Exception();
;            }
        }
    }

    public interface IResourceProvider
    {
        Sprite GetSpriteForMenuData(IActionMenuData data);
    }
