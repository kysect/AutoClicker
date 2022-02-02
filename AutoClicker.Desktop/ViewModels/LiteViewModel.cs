using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AutoClicker.Desktop.Commands;
using AutoClicker.Lite;

namespace AutoClicker.Desktop.ViewModels;

public class LiteViewModel : MainViewModel
{
    private readonly LiteModel _model;
    private LiteService _service;

    public LiteViewModel()
    {
        _model = new LiteModel();

        StartExecution = new BaseCommand(OnStartExecution);
        StartPicking = new BaseCommand(OnStartPicking);

        _model.PropertyChanged +=
            (_, e) => OnPropertyChanged(e.PropertyName);
    }

    internal void OnWindowLoaded(object sender, RoutedEventArgs e) =>
        _service = new LiteService(_model);

    internal void OnWindowClosing(object sender, CancelEventArgs e) =>
        _service.Dispose();


    public uint Duration
    {
        get => _model.Duration;
        set => _model.Duration = value;
    }

    public uint ClicksPerSecond
    {
        get => _model.ClicksPerSecond;
        set => _model.ClicksPerSecond = value;
    }

    public uint Delay
    {
        get => _model.Delay;
        set => _model.Delay = value;
    }

    public string SelectedClickType
    {
        get => _model.SelectedClickType;
        set => _model.SelectedClickType = value;
    }

    public int XCoordinate
    {
        get => _model.XCoordinate;
        set => _model.XCoordinate = value;
    }

    public int YCoordinate
    {
        get => _model.YCoordinate;
        set => _model.YCoordinate = value;
    }

    public bool UserCursorActive
    {
        get => _model.UserCursorActive;
        set => _model.UserCursorActive = value;
    }

    public bool CoordinatesActive
    {
        get => _model.CoordinatesActive;
        set => _model.CoordinatesActive = value;
    }

    public string StartButtonContent
    {
        get => _model.StartButtonContent;
        set => _model.StartButtonContent = value;
    }

    public string StartButtonBackGround
    {
        get => _model.StartButtonBackGround;
        set => _model.StartButtonBackGround = value;
    }

    public ObservableCollection<string> ClickTypes => _model.ClickTypes;

    public ICommand StartExecution { get; }

    public ICommand StartPicking { get; }

    private async void OnStartExecution(object obj) =>
        await _service.StartExecution();

    private void OnStartPicking(object obj) =>
        _service.StartPicking();
}
