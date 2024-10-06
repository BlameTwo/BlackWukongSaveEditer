using System;
using System.Threading.Tasks;
using ArchiveB1;
using b1;
using BlackWukongSaveEditer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Protobuf;
using Microsoft.UI.Xaml;
using Windows.Storage.Pickers;

namespace BlackWukongSaveEditer.ViewModels;

public sealed partial class GameSaveViewModel : ObservableObject
{
    [RelayCommand]
    async Task SelectFile()
    {
        Data = new GameSaveData();
        FileOpenPicker picker = new FileOpenPicker();
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
        picker.ViewMode = PickerViewMode.Thumbnail;
        picker.FileTypeFilter.Add(".sav");
        var result = await picker.PickSingleFileAsync();
        if (result != null)
        {
            var buffer = await Windows.Storage.FileIO.ReadBufferAsync(result);
            using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
            {
                var bytes = new byte[buffer.Length];
                dataReader.ReadBytes(bytes);
                IMessage<ArchiveFile> info = new ArchiveFile();
                info.MergeFrom(bytes);
                if (info is ArchiveFile file)
                {
                    var contentBytes = file.GameArchivesDataBytes.ToByteArray();
                    FUStBEDArchivesData fUStBEDArchivesData =
                        BGW_GameArchiveMgr.DeserializeArchiveDataFromBytes<FUStBEDArchivesData>(
                            true,
                            contentBytes
                        );
                    this.Data.GetBase(fUStBEDArchivesData).GetMaps(fUStBEDArchivesData);
                }
            }
        }
        this.OpenSaveVis = Visibility.Collapsed;
        this.SaveDataVis = Visibility.Visible;
    }

    [ObservableProperty]
    GameSaveData data;

    [ObservableProperty]
    Visibility openSaveVis = Visibility.Visible;

    [ObservableProperty]
    Visibility saveDataVis = Visibility.Collapsed;
}
