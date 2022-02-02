using AutoClicker.Core;
using AutoClicker.Core.Services;

namespace AutoClicker.Full.Actions;

public class MoveAction : IAction
{
    private MousePoint _mousePoint;

    public bool TryInitialize(string input)
    {
        var coordsContent = input
            .Replace(" ", "")
            .Split(',');

        if (coordsContent.Length != 2
            || !(int.TryParse(coordsContent[0], out int x)
                 && int.TryParse(coordsContent[1], out int y))) 
            return false;

        _mousePoint = new MousePoint(x, y);

        return true;
    }

    public void Execute() =>
        new CursorService().SetPosition(_mousePoint);
}