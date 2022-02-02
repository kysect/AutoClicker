using AutoClicker.Core;
using System;

namespace AutoClicker.Lite.Models;

public class ExecutionData
{
    internal ExecutionData(
        TimeSpan duration,
        uint clicksPerSecond,
        TimeSpan startDelay,
        MouseMessages clickType,
        bool userCursorActive,
        MousePoint cursorPosition)
    {
        StartDelay = startDelay;
        ClickType = clickType;
        UserCursorActive = userCursorActive;
        CursorPosition = cursorPosition;

        SleepTime = TimeSpan.FromMilliseconds(1000 / clicksPerSecond - 3);
        CycleCount = (uint)(clicksPerSecond * duration.TotalSeconds);
    }

    public TimeSpan StartDelay { get; }

    public MouseMessages ClickType { get; }

    public bool UserCursorActive { get; }

    public MousePoint CursorPosition { get; }

    public TimeSpan SleepTime { get; }

    public uint CycleCount { get; }

    public static ExecutionData FromModel(LiteModel model) =>
        new(TimeSpan.FromMilliseconds(model.Duration), 
            model.ClicksPerSecond, 
            TimeSpan.FromMilliseconds(model.Delay), 
            (MouseMessages)Enum.Parse(typeof(MouseMessages), model.SelectedClickType),
            model.UserCursorActive, 
            new MousePoint(model.XCoordinate, model.YCoordinate));
}