using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoClicker.WorkWithDll.Listener
{
    public enum Wm
    {
        MouseMove = 0x0200,
        LeftDown = 0x0201,
        LeftUp = 0x0202,
        RightDown = 0x0204,
        RightUp = 0x0205,
        MiddleDown = 0x0207,
        MiddleUp = 0x0208,
        MouseWheel = 0x020A,
        XDown = 0x020B,
        XUp = 0x020C
    }
    
    public static class MouseConverter
    {
        public static MouseMessages GetMouseMessage(Wm inputType, IntPtr lParam)
        {
            if (Enum.TryParse(inputType.ToString(), false, out MouseMessages mouseMessage))
            {
                return mouseMessage;
            }
            
            var mInput = (MouseInput)
                Marshal.PtrToStructure(lParam, typeof(MouseInput));

            if (inputType == Wm.MouseWheel)
            {
                return ((int) mInput.MouseData > 0) ?
                    MouseMessages.WheelUp : MouseMessages.WheelDown;
            }

            throw new ArgumentException($"There is no input typed {inputType}");
        }
    }
}
