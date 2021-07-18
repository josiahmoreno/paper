using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TesterTargetViewInstaller", menuName = "TargetView/TesterTargetViewInstaller", order = 0)]
public class TesterTargetViewInstaller : ScriptableObjectInstaller<TesterTargetViewInstaller>
{

    [Inject] public IBattleProvider BattleProvider;
    public override void InstallBindings()
    {

        Container.BindInstance(BattleProvider.Battle.TargetSystem);
        Container.BindInstance(BattleProvider.Battle.Enemies[0]);
    }


}
