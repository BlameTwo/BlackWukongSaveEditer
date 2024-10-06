using BlackWukongSaveEditer.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BlackWukongSaveEditer.Views;

public sealed partial class GameSavePage : Page
{
    public GameSavePage(ViewModels.GameSaveViewModel gameSaveViewModel)
    {
        this.InitializeComponent();
        this.ViewModel = gameSaveViewModel;
        this.Loaded += GameSavePage_Loaded;
    }

    private void GameSavePage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        this.title.UpDate();
    }

    public GameSaveViewModel ViewModel { get; }
}
