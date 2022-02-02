using System.Collections.Generic;
using AutoClicker.Core.Services.Models;
using AutoClicker.Core.Services.WorkWithDll;

namespace AutoClicker.Core.Services;

public class MouseService
{
    private const uint WheelValue = 60;

    private static readonly Dictionary<MouseMessages, Click> ClicksInfo =
        new()
        {
            { MouseMessages.Left, new Click(MouseMessages.LeftDown, MouseMessages.LeftUp) },
            { MouseMessages.Middle, new Click(MouseMessages.MiddleDown, MouseMessages.MiddleUp) },
            { MouseMessages.Right, new Click(MouseMessages.RightDown, MouseMessages.RightUp) },
            { MouseMessages.XButton, new Click(MouseMessages.XDown, MouseMessages.XUp) }
        };

    public void SendClick(MouseMessages clickType)
    {
        var click = ClicksInfo[clickType];
        click.MouseDown();
        click.MouseUp();
    }

    public void SendMessage(MouseMessages mouseMessage)
    {
        if (ClicksInfo.ContainsKey(mouseMessage))
        {
            SendClick(mouseMessage);
            return;
        }

        var input = new Input
        {
            type = 0
        };

        switch (mouseMessage)
        {
            case MouseMessages.WheelDown:
                input.U.mi.DwFlags = MouseMessages.Wheel;
                unchecked { input.U.mi.MouseData = (uint)-WheelValue; }
                break;

            case MouseMessages.WheelUp:
                input.U.mi.DwFlags = MouseMessages.Wheel;
                input.U.mi.MouseData = WheelValue;
                break;

            default:
                input.U.mi.DwFlags = mouseMessage;
                break;
        }

        InputSimulation.SendInput(1, new[] { input }, Input.Size);
    }

    internal void SendSimpleMessage(MouseMessages mouseMessage)
    {
        var input = new Input
        {
            type = 0
        };

        input.U.mi.DwFlags = mouseMessage;
        InputSimulation.SendInput(1, new[] { input }, Input.Size);
    }
}