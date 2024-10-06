using System.Collections.ObjectModel;
using BlackWukongSaveEditer.Models.UI;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BlackWukongSaveEditer.Models;

public partial class GameSaveData : ObservableObject
{
    [ObservableProperty]
    public string name;

    [ObservableProperty]
    int level;

    [ObservableProperty]
    string rrotoTag;

    [ObservableProperty]
    int samsara;

    [ObservableProperty]
    double maney;

    [ObservableProperty]
    int playerDeathCount;

    [ObservableProperty]
    ObservableCollection<MapItem> maps;
}
