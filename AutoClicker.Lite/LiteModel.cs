using AutoClicker.Common.Tools;
using System.Collections.ObjectModel;
using AutoClicker.Common.Settings;

namespace AutoClicker.Lite;

public class LiteModel : Notifier
{
    private int _xCoordinate;
    private int _yCoordinate;

    private string _startButtonContent;
    private string _startButtonBackGround;

    private bool _userCursorActive;

    public LiteModel()
    {
        ClickTypes = new ObservableCollection<string>
        {
            "Left",
            "Right",
            "Middle"
        };

        Duration = 1000;
        ClicksPerSecond = 100;
        SelectedClickType = ClickTypes[0];

        SetStartContent();
    }

    public uint Duration { get; set; }

    public uint ClicksPerSecond { get; set; }

    public uint Delay { get; set; }

    public string SelectedClickType { get; set; }

    public int XCoordinate
    {
        get => _xCoordinate;
        set => Update(ref _xCoordinate, value);
    }

    public int YCoordinate
    {
        get => _yCoordinate;
        set => Update(ref _yCoordinate, value);
    }

    public bool UserCursorActive
    {
        get => _userCursorActive;
        set => Update(ref _userCursorActive, value, nameof(CoordinatesActive));
    }

    public bool CoordinatesActive
    {
        get => !_userCursorActive;
        set => _userCursorActive = !value;
    }

    public string StartButtonContent
    {
        get => _startButtonContent;
        set => Update(ref _startButtonContent, value);
    }

    public string StartButtonBackGround
    {
        get => _startButtonBackGround;
        set => Update(ref _startButtonBackGround, value);
    }

    public ObservableCollection<string> ClickTypes { get; }

    internal void SetStartContent() => SetContent(
        $"Start ({AppSettings.GetInstance.StartButton})",
        "#FF59F109");

    internal void SetStopContent() => SetContent(
        $"Stop ({AppSettings.GetInstance.StopButton})",
        "#FFF71919");

    private void SetContent(string content, string backgroundColor) =>
        (StartButtonContent, StartButtonBackGround) = (content, backgroundColor);
}