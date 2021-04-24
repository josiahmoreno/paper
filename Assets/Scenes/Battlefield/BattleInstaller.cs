using UnityEngine;
using Zenject;

public class BattleInstaller : MonoInstaller
{
    [SerializeField] 
    GameBattleProvider _provider;
    public override void InstallBindings()
    {
        //Container.Bind<IInitializable>().To<GameBattleProvider>().AsSingle();
        //Container.Bind<IInitializable>().To<GameBattleProvider>().;
            Container.BindInterfacesAndSelfTo<GameBattleProvider>().FromInstance(this._provider).AsSingle().NonLazy();
        //Container.Inject(this);
        var prov =  Container.Resolve<GameBattleProvider>();
        Container.Bind<Battle.Battle>().FromInstance(_provider.Battle).NonLazy();
        Container.Bind<IBattleFieldViewModel>().To<BattlefieldViewModel>().AsTransient();

    }
    
    
}

