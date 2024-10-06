using BlackWukongSaveEditer.ViewModels;
using BlackWukongSaveEditer.Views;
using Microsoft.UI.Xaml;
using WinUIEx;

namespace BlackWukongSaveEditer;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Window = new MainWindow();
        var page = new GameSavePage(new GameSaveViewModel());
        Window.Content = page;
        Window.SetIsMaximizable(false);
        var manager = WindowManager.Get(Window);
        manager.MinHeight = 700;
        manager.MaxHeight = 700;
        manager.MinWidth = 500;
        manager.MaxWidth = 500;
        Window.SetIsResizable(true);
        page.title.Window = Window;
        Window.Activate();
        page.title.UpDate();
    }

    public static Window Window { get; private set; }
}
