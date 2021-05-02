using System.Collections.ObjectModel;
using System.ComponentModel;

public interface IBattleFieldViewModel: IViewModelBase
{
    Battle.Battle Battle { get; }
    ObservableCollection<CharacterEntity> CharacterEntities { get; }
}

public interface IViewModelBase: INotifyPropertyChanged
{
    
}