using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoClicker.WorkWithDll.InputSimulation;

namespace AutoClicker.Information.FullInfo.Actions
{
    public class KeyAction : IAction
    {
        private Key _key;
        private bool _keyDown;
        public bool TryParse(string input)
        {
            var keyContent = input.Split(", ");
            if (keyContent.Length != 2)
                return false;

            if (!Enum.TryParse(keyContent[0], out _key))
                return false;

            switch (keyContent[1])
            {
                case "down":
                    _keyDown = true;
                    break;
                case "up":
                    _keyDown = false;
                    break;
                default:
                    return false;
            };
            return true;
        }
        public void Execute()
        {
            SimulateInput.KeyboardEvent(_key, _keyDown);
        }
    }
}
