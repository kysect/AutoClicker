using System;
using AutoClicker.Core;
using AutoClicker.Core.Services;

namespace AutoClicker.Full.Actions;

public class MouseAction : IAction
{
    private MouseMessages _mouseMessage;

    public bool TryInitialize(string input) =>
        Enum.TryParse(input, out _mouseMessage);
    

    public void Execute() =>
        new MouseService().SendMessage(_mouseMessage);
    
}