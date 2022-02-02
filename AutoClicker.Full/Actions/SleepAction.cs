using System.Threading;

namespace AutoClicker.Full.Actions;

public class SleepAction : IAction
{
    private int _sleepTimeMs;

    public bool TryInitialize(string input) =>
        int.TryParse(input, out _sleepTimeMs);
    
    public void Execute() =>
        Thread.Sleep(_sleepTimeMs);
}

