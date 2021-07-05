using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Xml.Serialization;
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
            using var sw = new StreamWriter(filename);
            var xmls = new XmlSerializer(typeof(MySettings));
            xmls.Serialize(sw, this);
        }
        public static MySettings Load(string filename)
        {
            using var sr = new StreamReader(filename);
            var xmls = new XmlSerializer(typeof(MySettings));
            return (MySettings)xmls.Deserialize(sr);
        }
    }
}
