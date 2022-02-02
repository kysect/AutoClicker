using AutoClicker.Common.Settings;
using AutoClicker.Core.EventArguments;

namespace AutoClicker.Common.Extensions;

public static class KeyPressedArgsExtensions
{
    public static bool CanAccessExecution(this KeyPressedArgs args, bool actionInProgress) => 
        actionInProgress
            ? args.KeyPressed == AppSettings.GetInstance.StopButton
            : args.KeyPressed == AppSettings.GetInstance.StartButton;
}