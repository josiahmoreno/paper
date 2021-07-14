using UnityEngine;
using Zenject;


[CreateAssetMenu(fileName = "GameBattleInstaller", menuName = "Installers/GameBattleInstaller", order = 0)]
public class GameBattleInstaller: ScriptableObjectInstaller<GameBattleInstaller>
{
    public GameBattleScriptableObject GameBattleScriptableObject;

    public override void InstallBindings()
    {
        Container.Bind<IBattleProvider>().FromInstance(GameBattleScriptableObject);
        Container.Bind<GameBattleScriptableObject>().FromInstance(GameBattleScriptableObject);
        Container.Bind<Battle.Battle>().FromInstance(GameBattleScriptableObject.Battle);
    }
}