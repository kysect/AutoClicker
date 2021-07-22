using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoClicker.Commands;
using AutoClicker.Information;
using AutoClicker.Views;

namespace AutoClicker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public static void OpenNewWindow(Windows windowType)
        {
            switch (windowType)
            {
                case Windows.Lite:
                    new LiteWindow().Show();
                    return;
                case Windows.Full:
                    new FullWindow().Show();
                    return;
                case Windows.Menu:
                    new MenuWindow().Show();
                    return;
                default:
                    throw new ArgumentException(
                        $"Window {windowType} not founded");
            }
        }

        public MainViewModel()
        {
            OpenWindow = new BaseCommand(OnOpenWindow);
        }
        public Action CloseAction { get; set; }
        public ICommand OpenWindow { get; }
        protected virtual void OnOpenWindow(object obj)
        {
            if (!Enum.TryParse((string)obj, true, out Windows windowType))
            {
                throw new ArgumentException(
                    $"Window {(string)obj} not founded");
            }
            OpenNewWindow(windowType);
            CloseAction();
        }
    }
}
