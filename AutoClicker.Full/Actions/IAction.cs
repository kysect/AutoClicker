namespace AutoClicker.Full.Actions;

public interface IAction
{
    bool TryInitialize(string input);

    void Execute();
}
