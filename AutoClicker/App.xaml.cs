using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using AutoClicker.Information;
using System.Windows.Navigation;

namespace AutoClicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string Filename = "settings.json";
        protected override void OnStartup(StartupEventArgs e)
        {
            MySettings.Settings = new MySettings();
            if (File.Exists(Filename))
                MySettings.Settings = MySettings.Load(Filename);
            base.OnStartup(e);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Actions.OpenWindow(MySettings.Settings.LastOpenedWindow);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            MySettings.Settings.Save(Filename);
            base.OnExit(e);
        }
    }
}
