using System;
using System.Runtime.InteropServices;
using AutoClicker.Core.Listener.Types;

namespace AutoClicker.Core.Listener.Converters;

internal static class MouseConverter
{
    internal static MouseMessages GetMouseMessage(Wm inputType, IntPtr lParam)
    {
        if (Enum.TryParse(inputType.ToString(), false, out MouseMessages mouseMessage))
            return mouseMessage;

        var mInput = (MouseInput)(Marshal.PtrToStructure(lParam, typeof(MouseInput))
                                  ?? throw new NullReferenceException("Param was no marshaled"));

        if (inputType is Wm.MouseWheel)
            return (int) mInput.MouseData > 0
                ? MouseMessages.WheelUp
                : MouseMessages.WheelDown;
        
        throw new ArgumentException($"There is no input typed {inputType}");
    }
}

