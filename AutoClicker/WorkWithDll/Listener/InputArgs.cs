using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoClicker.WorkWithDll.Listener
{
    public class KeyPressedArgs : EventArgs
    {
        public Key KeyPressed { get; set; }
        public bool KeyDown { get; set; }
        public KeyPressedArgs(Key key, bool keyDown)
        {
            KeyPressed = key;
            KeyDown = keyDown;
        }
    }

    public class MouseArgs : EventArgs
    {
        public MouseMessages Message { get; set; }
        public MouseArgs(MouseMessages message)
        {
            Message = message;
        }
    }
}
