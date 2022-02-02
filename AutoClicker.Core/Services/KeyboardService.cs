using System.Windows.Input;
using AutoClicker.Core.Services.WorkWithDll;

namespace AutoClicker.Core.Services;

public class KeyboardService
{
    public void SendKeystroke(Key key)
    {
        SendKey(key, true);
        SendKey(key, false);
    }

    public void SendKey(Key key, bool isKeyDown)
    {
        var input = new Input
        {
            type = 1
        };

        input.U.ki.wVk = (short)KeyInterop.VirtualKeyFromKey(key);
        input.U.ki.dwFlags = GetEvent(isKeyDown);

        InputSimulation.SendInput(1, new[] { input }, Input.Size);
    }

    public void SendKeystroke(char c)
    {
        SendKey(c, true);
        SendKey(c, false);
    }

    public void SendKey(char c, bool isKeyDown)
    {
        var input = new Input
        {
            type = 1
        };

        input.U.ki.wScan = (short)c;
        input.U.ki.dwFlags = KeyEventF.Unicode | GetEvent(isKeyDown);

        InputSimulation.SendInput(1, new[] { input }, Input.Size);
    }

    private static KeyEventF GetEvent(bool isKeyDown) => isKeyDown
        ? KeyEventF.KeyDown
        : KeyEventF.KeyUp;
}