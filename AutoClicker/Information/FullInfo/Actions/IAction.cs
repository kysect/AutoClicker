using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker.Information.FullInfo.Actions
{
    public interface IAction
    {
        bool TryParse(string input);
        void Execute();
    }
}
