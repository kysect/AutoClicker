using System;
using System.Windows;
using System.Windows.Input;
using AutoClicker.Common.Settings.Types;
using AutoClicker.Common.Tools;
using AutoClicker.Desktop.Commands;
using AutoClicker.Desktop.Views;

namespace AutoClicker.Desktop.ViewModels;

public class MainViewModel : Notifier
{
    public MainViewModel() =>
        OpenWindow = new BaseCommand(OnOpenWindow);

    public Action CloseAction { get; set; }

    public ICommand OpenWindow { get; }

    public static void OpenNewWindow(WindowType windowType)
    {
        Window window = windowType switch
        {
            WindowType.Lite => new LiteWindow(),
            WindowType.Full => new FullWindow(),
            WindowType.Menu => new MenuWindow(),
            _ => throw new ArgumentException(
                $"Window {windowType} was not founded")
        };

        window.Show();
    }

    protected virtual void OnOpenWindow(object obj)
    {
        if (obj is not string str
            || !Enum.TryParse(str, true, out WindowType windowType)) 
            throw new ArgumentException(
                "Window was not founded");

        OpenNewWindow(windowType);
        CloseAction();
    }
}

