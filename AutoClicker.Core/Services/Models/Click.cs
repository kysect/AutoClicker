namespace AutoClicker.Core.Services.Models;

internal class Click
{
    private readonly MouseMessages _mouseDown;
    private readonly MouseMessages _mouseUp;

    internal Click(MouseMessages mouseDown, MouseMessages mouseUp) =>
        (_mouseDown, _mouseUp) = (mouseDown, mouseUp);

    internal void MouseDown() =>
        new MouseService().SendSimpleMessage(_mouseDown);

    internal void MouseUp() =>
        new MouseService().SendSimpleMessage(_mouseUp);
}