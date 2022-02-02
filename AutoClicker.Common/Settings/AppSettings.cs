using System.Windows.Input;
using System.IO;
using System.Text.Json;
using AutoClicker.Common.Settings.Types;

namespace AutoClicker.Common.Settings;

public class AppSettings
{
    private const string Filename = "settings.json";

    private static readonly object Locker = new ();

    private static AppSettings _instance;

    public Key StartButton { get; set; }
    public Key StopButton { get; set; }
    public WindowType LastOpenedWindow { get; set; }

    public static AppSettings GetInstance
    {
        get
        {
            if (_instance is null)
                lock (Locker)
                    _instance ??= new AppSettings();

            return _instance;
        }
    }

    public static void Save() =>
        File.WriteAllText(Filename,
            JsonSerializer.Serialize(_instance));

    public static void Load() => 
        _instance = File.Exists(Filename) 
            ? JsonSerializer.Deserialize<AppSettings>(
                    File.ReadAllText(Filename))
            : DefaultSettings;

    private static AppSettings DefaultSettings => new()
    {
        StartButton = Key.F9,
        StopButton = Key.F10,
        LastOpenedWindow = WindowType.Menu
    };
}
