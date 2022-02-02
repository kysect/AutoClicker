using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using AutoClicker.Desktop.Commands;
using AutoClicker.Full;

namespace AutoClicker.Desktop.ViewModels;

public class FullViewModel : MainViewModel
{
    private readonly FullModel _model;
    private FullService _service;

    public FullViewModel()
    {
        _model = new FullModel();
        
        StartExecution = new BaseCommand(OnStartExecution);
        StartPicking = new BaseCommand(OnStartPicking);
        StartRecording = new BaseCommand(OnStartRecording);

        _model.PropertyChanged +=
            (_, e) => OnPropertyChanged(e.PropertyName);
    }

    internal void OnWindowLoaded(object sender, RoutedEventArgs e) =>
        _service = new FullService(_model);

    internal void OnWindowClosing(object sender, CancelEventArgs e) =>
        _service.Dispose();

    public string TextBoxInput
    {
        get => _model.TextBoxInput;
        set => _model.TextBoxInput = value;
    }

    public ICommand StartExecution { get; }

    public ICommand StartPicking { get; }

    public ICommand StartRecording { get; }

    private async void OnStartExecution(object obj) =>
        await _service.StartExecution();

    private void OnStartPicking(object obj) =>
        _service.StartPicking();

    private void OnStartRecording(object obj) =>
        _service.StartRecording();
}
