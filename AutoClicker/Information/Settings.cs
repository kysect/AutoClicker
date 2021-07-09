using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace AutoClicker.Information
{
    public class MySettings
    {
        public MySettings()
        {
            StartButton = Key.F9;
            StopButton = Key.F10;
            LastOpenedWindow = Windows.Menu;
        }
        public static MySettings Settings { get; set; }
        public Key StartButton { get; set; }
        public Key StopButton { get; set; }
        public Windows LastOpenedWindow { get; set; }
        public void Save(string filename)
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(filename, jsonString);
        }
        public static MySettings Load(string filename)
        {
            string jsonString = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<MySettings>(jsonString);
        }
    }
}
