using System.Runtime.InteropServices;

namespace AutoClicker.Core.Services.WorkWithDll;

internal static class InputSimulation
{
    [DllImport("user32.dll")]
    internal static extern void SendInput(
        uint nInputs,
        [MarshalAs(UnmanagedType.LPArray), In] Input[] pInputs,
        int cbSize);
}