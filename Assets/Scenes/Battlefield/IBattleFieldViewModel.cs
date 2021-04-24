using System.Collections.ObjectModel;
using System.ComponentModel;

public interface IBattleFieldViewModel: INotifyPropertyChanged
{
    Battle.Battle Battle { get; }
    ObservableCollection<CharacterEntity> CharacterEntities { get; }
}