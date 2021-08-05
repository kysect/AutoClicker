using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoClicker.WorkWithDll
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        internal uint type;
        internal InputUnion U;
        internal static int Size => Marshal.SizeOf(typeof(Input));
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct InputUnion
    {
        [FieldOffset(0)] internal MouseInput mi;
        [FieldOffset(0)] internal KeyboardInput ki;
        [FieldOffset(0)] internal HardwareInput hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct HardwareInput
    {
        internal int uMsg;
        internal short wParamL;
        internal short wParamH;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public MousePoint Point;
        public uint MouseData;
        public MouseMessages DwFlags;
        public uint Time;
        internal UIntPtr DwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;
        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardInput
    {
        internal short wVk;
        internal short wScan;
        internal KeyEventF dwFlags;
        internal int time;
        internal UIntPtr dwExtraInfo;
    }

    [Flags]
    internal enum KeyEventF : uint
    {
        KeyDown = 0x0000,
        ExtendedKey = 0x0001,
        KeyUp = 0x0002,
        ScanCode = 0x0008,
        Unicode = 0x0004
    }

    [Flags]
    public enum MouseMessages : uint
    {
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        WheelDown,
        WheelUp,
        Left,
        Right,
        Middle,
        XButton
    }
}
