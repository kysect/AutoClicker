using AutoClicker.Common.Settings;
using AutoClicker.Common.Settings.Types;
using AutoClicker.Desktop.ViewModels;

namespace AutoClicker.Desktop.Views;

public partial class MenuWindow
{
    public MenuWindow()
    {
        InitializeComponent();
        AppSettings.GetInstance.LastOpenedWindow = WindowType.Menu;

        var vm = new MainViewModel();
        DataContext = vm;
        vm.CloseAction ??= Close;
    }
}
