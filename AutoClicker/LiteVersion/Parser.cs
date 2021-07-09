using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using AutoClicker.Information;
using AutoClicker.WorkWithDll;

namespace AutoClicker.LiteVersion
{
    public class Parser
    {
        private const int MinClicksPerSecond = 1;
        private const int MaxClicksPerSecond = 100;
        public static List<string> TryParse(List<string> parameters, out AutoClickerInfo task)
        {
            var errorList = new List<string>();

            if (!int.TryParse(parameters[0], out int duration) ||
                duration < -1)
            {
                errorList.Add("duration");
            }

            if (!int.TryParse(parameters[1], out int clicksPerSecond) ||
                clicksPerSecond < MinClicksPerSecond ||
                clicksPerSecond > MaxClicksPerSecond)
            {
                errorList.Add("clicks per second");
            }

            if (!int.TryParse(parameters[2], out int startDelay) ||
                startDelay < 0)
            {
                errorList.Add("start delay");
            }

            if (!Enum.TryParse(parameters[3], out Clicks clickType))
            {
                errorList.Add("click type");
            }

            if (!bool.TryParse(parameters[4], out bool userCursorActive))
            {
                errorList.Add("capture of the user cursor");
            }

            int x = 0, y = 0;
            if (!userCursorActive)
            {
                if (!int.TryParse(parameters[5], out x) ||
                   !int.TryParse(parameters[6], out y) ||
                   x < 0 ||
                   y < 0)
                {
                    errorList.Add("coordinates");
                }
            }

            task = new AutoClickerInfo(
                TimeSpan.FromMilliseconds(duration),
                clicksPerSecond,
                TimeSpan.FromMilliseconds(startDelay),
                clickType,
                userCursorActive,
                new MouseClicks.MousePoint(x, y));

            return errorList;
        }
    }
}
