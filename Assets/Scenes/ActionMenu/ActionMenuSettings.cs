
using UnityEngine;

namespace Scenes.ActionMenu
{
    [CreateAssetMenu(fileName = "ActionMenuSettings", menuName = "ActionMenu/ActionMenuSettings", order = 0)]
    public class ActionMenuSettings: ScriptableObject, IActionMenuSettings
    {
        public GameObject _ActionViewPrefab;
        public GameObject ActionViewPrefab => _ActionViewPrefab;

        public IResourceProvider ResourceProvider => ActionMenuResourcesScriptableObject;

        public ActionMenuResourcesScriptableObject ActionMenuResourcesScriptableObject;
    }

    public interface IActionMenuSettings
    {
        public GameObject ActionViewPrefab { get; }
        
        public IResourceProvider ResourceProvider { get;  }
    }
}