using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoClicker.FullVersion;
using AutoClicker.LiteVersion;
using AutoClicker.WorkWithDll;

namespace AutoClicker.Information
{
    public enum Windows
    {
        Lite,
        Full,
        Menu
    }

    public class Actions
    {
        public static void OpenWindow(Windows windowType)
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
                    new MainWindow().Show();
                    return;
                default:
                    throw new ArgumentException(
                        $"Window {windowType} not founded");
            }
        }

    }
    public enum Clicks
    {
        Left,
        Right,
        Middle
    }
    public class AutoClickerInfo
    {
        public TimeSpan Duration { get; set; }
        public int ClicksPerSecond { get; set; }
        public TimeSpan StartDelay { get; set; }
        public Clicks ClickType { get; set; }
        public bool UserCursor { get; set; }
        public MouseClicks.MousePoint Point { get; set; }
    }
}
