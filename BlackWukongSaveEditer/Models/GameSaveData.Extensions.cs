using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ArchiveB1;
using b1;
using BlackWukongSaveEditer.Models.UI;
using GurGsPersistent;

namespace BlackWukongSaveEditer.Models;

public static class GameSaveDataExtension
{
    public static GameSaveData GetBase(
        this GameSaveData data,
        FUStBEDArchivesData uStBEDArchivesData
    )
    {
        data.Name = uStBEDArchivesData.RoleData.RoleCs.Base.Name;
        data.Level = uStBEDArchivesData.RoleData.RoleCs.Base.Level;
        data.Maney = Math.Round(
            (double)uStBEDArchivesData.RoleData.RoleCs.Bag.MoneyList.Last().MoneyValue / 10000,
            2
        );
        data.Samsara = uStBEDArchivesData.RoleData.RoleCs.Actor.NewGamePlusCount;
        data.PlayerDeathCount = uStBEDArchivesData
            .PersistentECSData
            .BGCData
            .BGCPlayerDeathData
            .PlayerDeathCount;
        return data;
    }

    public static GameSaveData GetMaps(
        this GameSaveData data,
        FUStBEDArchivesData uStBEDArchivesData
    )
    {
        data.Maps = new();
        var list =
            uStBEDArchivesData.PersistentECSData.BPCData.BPCRebirthPointData.ActivedRebirthPointList.ToDictionary(
                x => x.Value,
                x => x.HasValue
            );
        var config = new GameConfigItem();
        ObservableCollection<MapItem> mapResult = new ObservableCollection<MapItem>();
        foreach (var item in config.GameMaps)
        {
            mapResult.Add(new MapItem() { DisplayName = item, Points = new() });
        }
        foreach (var item in config.MapItems)
        {
            MapPointItem pointItem = new MapPointItem();
            var value = list.Select(x => x.Key == item.Key).Where(x => x == true).Any();
            pointItem.Id = item.Key;
            pointItem.DisplayName = item.Value.Item1;
            pointItem.IsOpen = value;
            foreach (var mapItem in mapResult)
            {
                var bitMapkey = config.BigMapsItems.Where(x =>
                    x.Value.Item2 == mapItem.DisplayName
                );
                if (bitMapkey.Where(x => x.Key == item.Value.Item2).Any())
                {
                    pointItem.BigMapName = bitMapkey
                        .Where(x => x.Key == item.Value.Item2)
                        .First()
                        .Value.Item1;
                    if (
                        mapItem.DisplayName
                        == bitMapkey.Where(x => x.Key == item.Value.Item2).First().Value.Item2
                    )
                    {
                        mapItem.Points.Add(pointItem);
                    }
                }
            }
        }
        data.Maps = mapResult;
        return data;
    }
}
