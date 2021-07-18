using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "TargetSystemInstaller", menuName = "Installers/TargetSystemInstaller", order = 0)]
public class TargetSystemInstaller : ScriptableObjectInstaller<TargetSystemInstaller>
{

    [Inject] public IBattleProvider BattleProvider;
    public TargetViewInstaller TargetViewInstaller;
    public override void InstallBindings()
    {
        Container.BindFactory<Enemy, TargetView, TargetView.Factory>()
                .To<TargetView>()
                .FromSubContainerResolve()
                .ByInstaller<TargetViewInstaller>().AsCached();
                
               // .ByInstaller(TargetViewInstaller);
  
    }


}
