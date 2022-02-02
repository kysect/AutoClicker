using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using AutoClicker.Core.EventArguments;
using AutoClicker.Core.Listener.Converters;
using AutoClicker.Core.Listener.Types;

namespace AutoClicker.Core.Listener;

public class LowLevelListener
{
    private const int WhKeyboardLl = 13;
    private const int WhMouseLl = 14;

    private const int WmKeyDown = 0x0100;
    private const int WmSystemKeyDown = 0x0104;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    public event EventHandler<KeyPressedArgs> OnKeyPressed;
    public event EventHandler<MouseArgs> OnMouseMessage;
    public event EventHandler OnMouseMoved;

    private readonly LowLevelKeyboardProc _keyboardProc;
    private readonly LowLevelMouseProc _mouseProc;

    private IntPtr _keyboardHookId = IntPtr.Zero;
    private IntPtr _mouseHookId = IntPtr.Zero;

    public LowLevelListener()
    {
        _keyboardProc = KeyboardHookCallback;
        _mouseProc = MouseHookCallback;
    }

    public void SetHook()
    {
        _keyboardHookId = SetHook(_keyboardProc);
        _mouseHookId = SetHook(_mouseProc);
    }

    public void UnHook()
    {
        UnhookWindowsHookEx(_keyboardHookId);
        UnhookWindowsHookEx(_mouseHookId);
    }

    private static IntPtr SetHook(LowLevelMouseProc proc)
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule;

        return SetWindowsHookEx(WhMouseLl, proc, 
            GetModuleHandle(curModule!.ModuleName), 0);
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule;

        return SetWindowsHookEx(WhKeyboardLl, proc, 
            GetModuleHandle(curModule!.ModuleName), 0);
    }

    private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            bool keyDown = wParam == (IntPtr)WmKeyDown ||
                           wParam == (IntPtr)WmSystemKeyDown;

            int vkCode = Marshal.ReadInt32(lParam);

            OnKeyPressed?.Invoke(this,
                new KeyPressedArgs(KeyInterop
                    .KeyFromVirtualKey(vkCode), keyDown));
        }

        return CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
    }

    private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            var inputType = (Wm)wParam;

            if (inputType is Wm.MouseMove)
            {
                OnMouseMoved?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnMouseMessage?.Invoke(this,
                        new MouseArgs(MouseConverter
                            .GetMouseMessage(inputType, lParam)));
            }
        }

        return CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
    }
}

