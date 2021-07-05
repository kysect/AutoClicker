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
using AutoClicker.LiteVersion;
using AutoClicker.FullVersion;
using AutoClicker.Information;

namespace AutoClicker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MySettings.Settings.LastOpenedWindow = Windows.Menu;
        }

        private void LiteButton_Click(object sender, RoutedEventArgs e)
        {
            var lite = new LiteWindow();
            lite.Show();
            Close();
        }

        private void FullButton_Click(object sender, RoutedEventArgs e)
        {
            var full = new FullWindow();
            full.Show();
            Close();
        }
    }
}
