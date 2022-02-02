using System.Runtime.InteropServices;

namespace AutoClicker.Core.Services;

public class CursorService
{
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    public void SetPosition(MousePoint point) =>
        SetCursorPos(point.X, point.Y);
    
    public MousePoint GetPosition()
    {
        if (!GetCursorPos(out MousePoint currentMousePoint))
            currentMousePoint = new MousePoint(0, 0);
        
        return currentMousePoint;
    }
}