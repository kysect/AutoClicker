using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AutoClicker.Common.Extensions;
using AutoClicker.Core.EventArguments;
using AutoClicker.Core.Listener;
using AutoClicker.Core.Services;
using AutoClicker.Full.Actions;
using AutoClicker.Full.Parsers;
using AutoClicker.Full.Recorders;

namespace AutoClicker.Full;

public class FullService : IDisposable
{
    private bool _disposedValue;

    private bool _pickingInProgress;
    private bool _actionInProgress;
    private bool _recordingInProgress;

    private readonly LowLevelListener _listener;

    private readonly CursorService _cursorService;
    private readonly MouseService _mouseService;
    private readonly KeyboardService _keyboardService;

    private readonly FullModel _model;

    private readonly ActionRecorder _actionRecorder;

    public FullService(FullModel model)
    {
        _listener = new LowLevelListener();
        _listener.OnKeyPressed += ListenerOnKeyPressed;
        _listener.OnMouseMessage += ListenerOnMouseMessage;
        _listener.OnMouseMoved += ListenerOnMouseMoved;
        _listener.SetHook();

        _cursorService = new CursorService();
        _mouseService = new MouseService();
        _keyboardService = new KeyboardService();

        _model = model;

        _actionRecorder = new ActionRecorder(_model, _cursorService);
    }

    public void Dispose()
    {
        if (_disposedValue)
            return;

        _listener.UnHook();

        _disposedValue = true;
    }

    public async Task StartExecution()
    {
        if (!_actionInProgress)
        {
            if (!new ActionParser(_model.TextBoxInput).TryParse(out List<IAction> actionList))
            {
                MessageBox.Show("Wrong input!");
                return;
            }

            _actionInProgress = true;

            await Task.Run(() => Execute(actionList));
        }

        _actionInProgress = false;
    }

    public void StartPicking() =>
        _pickingInProgress = true;

    public void StartRecording()
    {
        if (!_recordingInProgress)
            _actionRecorder.StartRecording();

        _recordingInProgress = !_recordingInProgress;
    }

    private async void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
    {
        if (_recordingInProgress)
            _actionRecorder.RecordKey(e);

        if (!e.KeyDown)
            return;

        if (e.CanAccessExecution(_actionInProgress))
            await StartExecution();
    }

    private void ListenerOnMouseMessage(object sender, MouseArgs e)
    {
        if (_recordingInProgress)
            _actionRecorder.RecordMouse(e);
    }

    private void ListenerOnMouseMoved(object sender, EventArgs e)
    {
        if (_recordingInProgress)
           _actionRecorder.RecordCursor();
    }

    private void Execute(List<IAction> actionList)
    {
        foreach (var action in actionList)
        {
            action.Execute();
            if (!_actionInProgress)
                return;
        }
    }
}