using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.WorkWithDll;
using AutoClicker.WorkWithDll.InputSimulation;

namespace AutoClicker.Information.FullInfo.Actions
{
    public class MouseAction : IAction
    {
        private MouseMessages _mouseMessage;
        public bool TryParse(string input)
        {
            return Enum.TryParse(input, out _mouseMessage);
        }
        public void Execute()
        {
            SimulateInput.MouseEvent(_mouseMessage);
        }
    }
}
