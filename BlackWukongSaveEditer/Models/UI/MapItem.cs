using System.Collections.ObjectModel;

namespace BlackWukongSaveEditer.Models.UI;

public class MapItem
{
    public string DisplayName { get; set; }

    public ObservableCollection<MapPointItem> Points { get; set; }
}

public class MapPointItem
{
    public int Id { get; set; }

    public string DisplayName { get; set; }

    public string BigMapName { get; set; }
    public bool IsOpen { get; set; }
}
