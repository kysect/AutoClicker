using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.WorkWithDll;
using AutoClicker.WorkWithDll.InputSimulation;

namespace AutoClicker.Information.FullInfo.Actions
{
    public class MoveAction : IAction
    {
        private MousePoint _mousePoint;

        public bool TryParse(string input)
        {
            var coordsContent = input.Replace(" ", "").Split(',');
            if (coordsContent.Length != 2)
                return false;

            if (!(int.TryParse(coordsContent[0], out int x) &&
                  int.TryParse(coordsContent[1], out int y)))
                return false;

            _mousePoint = new MousePoint(x, y);
            return true;
        }

        public void Execute()
        {
            MouseCursor.SetCursorPosition(_mousePoint);
        }
    }
}
