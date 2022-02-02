using System;
using System.Windows.Input;

namespace AutoClicker.Core.EventArguments;

public class KeyPressedArgs : EventArgs
{
    public KeyPressedArgs(Key key, bool keyDown) =>
        (KeyPressed, KeyDown) = (key, keyDown);

    public Key KeyPressed { get; set; }

    public bool KeyDown { get; set; }
}