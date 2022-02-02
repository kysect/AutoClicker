using System;
using System.Windows.Input;
using AutoClicker.Core.Services;
using AutoClicker.Full.Types;

namespace AutoClicker.Full.Actions;

public class KeyAction : IAction
{
    private Key _key;
    private bool _isKeyDown;

    public bool TryInitialize(string input)
    {
        var keyContent = input
            .Replace(" ", "")
            .Split(',');

        if (keyContent.Length != 2
            || !Enum.TryParse(keyContent[0], out _key)
            || !Enum.TryParse(keyContent[1], true, out KeyState keyState)) 
            return false;

        _isKeyDown = ResolveState(keyState);

        return true;
    }

    public void Execute() =>
        new KeyboardService().SendKey(_key, _isKeyDown);

    private static bool ResolveState(KeyState keyState) =>
        keyState switch
        {
            KeyState.Up => false,
            KeyState.Down => true,
            _ => throw new ArgumentOutOfRangeException(
                $"Failed to parse {keyState} as key state")
        };
}