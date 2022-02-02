using AutoClicker.Common.Settings;
using AutoClicker.Common.Settings.Types;
using AutoClicker.Desktop.ViewModels;

namespace AutoClicker.Desktop.Views;

public partial class FullWindow
{
    public FullWindow()
    {
        InitializeComponent();
        AppSettings.GetInstance.LastOpenedWindow = WindowType.Full;

        var vm = new FullViewModel();
        DataContext = vm;
        vm.CloseAction ??= Close;
        Loaded += vm.OnWindowLoaded;
        Closing += vm.OnWindowClosing;
    }
}