using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoClicker.Information;
using AutoClicker.ViewModels;
using AutoClicker.Views;

namespace AutoClicker.Views
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            MySettings.Settings.LastOpenedWindow = Windows.Menu;
            var vm = new MainViewModel();
            DataContext = vm;
            vm.CloseAction ??= () => Close();
        }
    }
}
