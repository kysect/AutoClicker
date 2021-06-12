using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.WorkWithDll;

namespace AutoClicker
{
    public class AutoClickerInfo
    {
        public TimeSpan Duration;
        public int ClicksPerSecond;
        public bool UserCursor;
        public MouseClicks.MousePoint Point;
    }
}
