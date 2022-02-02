using AutoClicker.Common.Settings;
using AutoClicker.Common.Settings.Types;
using AutoClicker.Desktop.ViewModels;

namespace AutoClicker.Desktop.Views;

public partial class LiteWindow
{
    public LiteWindow()
    {
        InitializeComponent();
        AppSettings.GetInstance.LastOpenedWindow = WindowType.Lite;

        var vm = new LiteViewModel();
        DataContext = vm;
        vm.CloseAction ??= Close;
        Loaded += vm.OnWindowLoaded;
        Closing += vm.OnWindowClosing;
    }
}