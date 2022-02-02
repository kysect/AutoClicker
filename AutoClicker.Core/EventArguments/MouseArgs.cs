using System;

namespace AutoClicker.Core.EventArguments;

public class MouseArgs : EventArgs
{
    public MouseArgs(MouseMessages message) =>
        Message = message;

    public MouseMessages Message { get; set; }
}