using System;
using Heroes;
using MenuData;
using UnityEngine;


    [CreateAssetMenu(fileName = "ResourceScriptableObject", menuName = "ResourceScriptableObject", order = 0)]
    public class ActionMenuResourcesScriptableObject : ScriptableObject, IResourceProvider
    {
        public Sprite Jump;
        public Sprite Strategies;
        public Sprite Item;
        public Sprite Hammer;
        public Sprite goombario;
        
        public Sprite GetSpriteForMenuData(Hero hero,IActionMenuData data)
        {
            switch (data.Name)
            {
                case "Jump":
                    return Jump;
                case "Strategies":
                    return Strategies;
                case "Items":
                    return Item;
                case "Hammer":
                    return Hammer;
                case "Abilities":
                    if (hero.Identity == Heroes.Heroes.Goombario)
                    {
                        return goombario;
                    }
                    else
                    {
                        return Strategies;
                    }
                default:
                    throw new Exception($"data: {data.Name} not found");
            }
        }
    }
