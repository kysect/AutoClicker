using System;
using AutoClicker.Core.EventArguments;
using AutoClicker.Core.Services;
using AutoClicker.Full.Types;

namespace AutoClicker.Full.Recorders;

public class ActionRecorder
{
    private DateTime _lastActionTime;

    private readonly FullModel _model;
    private readonly CursorService _cursorService;

    public ActionRecorder(FullModel model, CursorService cursorService) =>
        (_model, _cursorService) = (model, cursorService);

    public void StartRecording() =>
        _lastActionTime = DateTime.Now;

    public void RecordKey(KeyPressedArgs args) =>
        RecordAction(ActionType.Key,
            $"{args.KeyPressed}, {(args.KeyDown ? "down" : "up")}");

    public void RecordMouse(MouseArgs args) =>
        RecordAction(ActionType.Mouse, args.Message.ToString());

    public void RecordCursor()
    {
        var currentCoordinates = _cursorService.GetPosition();
        RecordAction(ActionType.Move,
            $"{currentCoordinates.X}, {currentCoordinates.Y}");
    }

    private void RecordAction(ActionType action, string info)
    {
        var currentTime = DateTime.Now;

        int msBetweenActions = (currentTime - _lastActionTime).Milliseconds;
        if (msBetweenActions is not 0)
            AddToTextBox(ActionType.Sleep, msBetweenActions.ToString());

        AddToTextBox(action, info);
        _lastActionTime = currentTime;
    }

    private void AddToTextBox(ActionType action, string info) =>
        _model.TextBoxInput += $"{action}({info});\n";
}