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
        Lite = 1,
        Full = 2,
        Menu = 3
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
        Left = 1,
        Right = 2,
        Middle = 3
    }
    public class AutoClickerInfo
    {
        public AutoClickerInfo(
            TimeSpan duration,
            int clicksPerSecond,
            TimeSpan startDelay,
            Clicks clickType,
            bool userCursorActive,
            MouseClicks.MousePoint point)
        {
            _duration = duration;
            _clicksPerSecond = clicksPerSecond;
            StartDelay = startDelay;
            ClickType = clickType;
            UserCursorActive = userCursorActive;
            Point = point;
        }

        private readonly TimeSpan _duration;
        private readonly int _clicksPerSecond;
        public TimeSpan StartDelay { get; }
        public Clicks ClickType { get; }
        public bool UserCursorActive { get; }
        public MouseClicks.MousePoint Point { get; }

        public TimeSpan SleepTime()
        {
            return TimeSpan.FromMilliseconds(1000 / _clicksPerSecond - 3);
        }
        public int Cycles()
        {
            return (int)(_clicksPerSecond * _duration.TotalSeconds);
        }
    }
}
