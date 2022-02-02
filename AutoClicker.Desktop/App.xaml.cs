using System.Windows;
using AutoClicker.Common.Settings;
using AutoClicker.Desktop.ViewModels;

namespace AutoClicker.Desktop;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        AppSettings.Load();
        base.OnStartup(e);
    }

    private void AppStartup(object sender, StartupEventArgs e)
    {
        MainViewModel.OpenNewWindow(AppSettings.GetInstance.LastOpenedWindow);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppSettings.Save();
        base.OnExit(e);
    }
}
