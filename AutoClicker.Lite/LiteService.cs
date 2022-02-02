using System;
using System.Threading;
using System.Threading.Tasks;
using AutoClicker.Common.Extensions;
using AutoClicker.Core.EventArguments;
using AutoClicker.Core.Listener;
using AutoClicker.Core.Services;
using AutoClicker.Lite.Models;

namespace AutoClicker.Lite;

public class LiteService : IDisposable
{
    private bool _disposedValue;

    private bool _pickingInProgress;
    private bool _actionInProgress;

    private readonly LowLevelListener _listener;

    private readonly CursorService _cursorService;
    private readonly MouseService _mouseService;

    private readonly LiteModel _model;

    public LiteService(LiteModel model)
    {
        _listener = new LowLevelListener();
        _listener.OnKeyPressed += ListenerOnKeyPressed;
        _listener.OnMouseMessage += ListenerOnMouseMessage;
        _listener.OnMouseMoved += ListenerOnMouseMoved;
        _listener.SetHook();

        _cursorService = new CursorService();
        _mouseService = new MouseService();

        _model = model;
    }

    public void Dispose()
    {
        if (_disposedValue)
            return;

        _listener.UnHook();

        _disposedValue = true;
    }

    public void StartPicking() => _pickingInProgress = true;

    public async Task StartExecution()
    {
        _model.SetStopContent();
        _actionInProgress = true;

        await Task.Run(() => Execute(ExecutionData.FromModel(_model)));

        _model.SetStartContent();
        _actionInProgress = false;
    }

    private void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
    {
        if (!e.KeyDown)
            return;
        
        if (_pickingInProgress)
        {
            _pickingInProgress = false;
            return;
        }

        if (e.CanAccessExecution(_actionInProgress))
            Task.Run(StartExecution);
    }

    private void ListenerOnMouseMessage(object sender, MouseArgs e)
    {
        if (_pickingInProgress)
            _pickingInProgress = false;
    }

    private void ListenerOnMouseMoved(object sender, EventArgs e)
    {
        if (!_pickingInProgress)
            return;
        
        var currentCoordinates = _cursorService.GetPosition();
        
        _model.XCoordinate = currentCoordinates.X;
        _model.YCoordinate = currentCoordinates.Y;
    }

    private void Execute(ExecutionData data)
    {
        var startPosition = _cursorService.GetPosition();

        Thread.Sleep(data.StartDelay);

        for (uint i = 0; i < data.CycleCount; i++)
        {
            if (!_actionInProgress)
                break;

            if (!data.UserCursorActive)
                _cursorService.SetPosition(data.CursorPosition);

            Thread.Sleep(1);

            _mouseService.SendClick(data.ClickType);

            Thread.Sleep(data.SleepTime);
        }

        if (!data.UserCursorActive)
            _cursorService.SetPosition(startPosition);
    }
}