using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClicker.Information.FullInfo.Actions
{
    public class SleepAction : IAction
    {
        private int _sleepTimeMs;
        public bool TryParse(string input)
        {
            return int.TryParse(input, out _sleepTimeMs);
        }
        public void Execute()
        {
            Thread.Sleep(_sleepTimeMs);
        }
    }
}
