using Scenes.Battlefield;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BattleFieldSettingsInstaller", menuName = "Installers/BattleFieldSettingsInstaller")]
public class BattleFieldSettingsInstaller : ScriptableObjectInstaller<BattleFieldSettingsInstaller>
{
    public IBattlefieldPositionerImpl.Settings Settings;
    public GameObject InputController;
    public override void InstallBindings()
    {
        Debug.Log($"{GetType().Name} - Install Bindings");
        Container.BindInstance(Settings);
        Container.Bind<BattleController>().FromComponentInNewPrefab(InputController).AsSingle().NonLazy();
    }
}