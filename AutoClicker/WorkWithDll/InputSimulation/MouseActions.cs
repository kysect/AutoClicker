using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker.WorkWithDll.InputSimulation
{
    public class Click
    {
        private readonly MouseMessages _mouseDown;
        private readonly MouseMessages _mouseUp;
        public void MouseDown()
        {
            SimulateInput.SimpleMouseEvent(_mouseDown);
        }
        public void MouseUp()
        {
            SimulateInput.SimpleMouseEvent(_mouseUp);
        }
        public Click(
            MouseMessages mouseDown,
            MouseMessages mouseUp)
        {
            _mouseDown = mouseDown;
            _mouseUp = mouseUp;
        }
    }

    public class MouseActions
    {
        public static readonly Dictionary<MouseMessages, Click> ClicksDictionary =
            new()
            {
                { MouseMessages.Left, new Click(MouseMessages.LeftDown, MouseMessages.LeftUp) },
                { MouseMessages.Middle, new Click(MouseMessages.MiddleDown, MouseMessages.MiddleUp) },
                { MouseMessages.Right, new Click(MouseMessages.RightDown, MouseMessages.RightUp) },
                { MouseMessages.XButton, new Click(MouseMessages.XDown, MouseMessages.XUp) }
            };

        public static void MouseClick(MouseMessages clickType)
        {
            var click = ClicksDictionary[clickType];
            click.MouseDown();
            click.MouseUp();
        }
    }
}
