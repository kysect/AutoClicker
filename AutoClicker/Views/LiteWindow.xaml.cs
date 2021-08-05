using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoClicker.WorkWithDll;
using AutoClicker.Information;
using AutoClicker.ViewModels;

namespace AutoClicker.Views
{
    public partial class LiteWindow : Window
    {
        public LiteWindow()
        {
            InitializeComponent();
            MySettings.Settings.LastOpenedWindow = Windows.Lite;

            var vm = new LiteViewModel();
            DataContext = vm;
            vm.CloseAction ??= () => Close();
            Loaded += vm.OnWindowLoaded;
            Closing += vm.OnWindowClosing;
        }
    }
}