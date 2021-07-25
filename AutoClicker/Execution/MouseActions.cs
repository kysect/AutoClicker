using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoClicker.Information;
using AutoClicker.WorkWithDll;

namespace AutoClicker.Execution
{
    public class Click
    {
        private readonly MouseEventFlags _mouseDown;
        private readonly MouseEventFlags _mouseUp;
        public void MouseDown()
        {
            MouseClicks.MouseEvent(_mouseDown);
        }
        public void MouseUp()
        {
            MouseClicks.MouseEvent(_mouseUp);
        }
        public Click(
            MouseEventFlags mouseDown,
            MouseEventFlags mouseUp)
        {
            _mouseDown = mouseDown;
            _mouseUp = mouseUp;
        }
        
    }
    public class MouseActions
    {
        private static readonly Dictionary<Clicks, Click> ClicksDependancy =
            new()
            {
                { Clicks.Left, new Click(MouseEventFlags.LeftDown, MouseEventFlags.LeftUp) },
                { Clicks.Middle, new Click(MouseEventFlags.MiddleDown, MouseEventFlags.MiddleUp) },
                { Clicks.Right, new Click(MouseEventFlags.RightDown, MouseEventFlags.RightUp) }
            };
        public static void MouseClick(Clicks clickType)
        {
            var click = ClicksDependancy[clickType];
            click.MouseDown();
            click.MouseUp();
        }
    }
}
