using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoClicker.WorkWithDll;

namespace AutoClicker.Information
{
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
            uint clicksPerSecond,
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
        private readonly uint _clicksPerSecond;
        public TimeSpan StartDelay { get; }
        public Clicks ClickType { get; }
        public bool UserCursorActive { get; }
        public MouseClicks.MousePoint Point { get; }

        public TimeSpan SleepTime()
        {
            return TimeSpan.FromMilliseconds(1000 / _clicksPerSecond - 3);
        }
        public uint Cycles()
        {
            return (uint)(_clicksPerSecond * _duration.TotalSeconds);
        }
    }
}
