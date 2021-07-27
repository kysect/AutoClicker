using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AutoClicker.Commands;
using AutoClicker.Execution;
using AutoClicker.Information;
using AutoClicker.WorkWithDll;

namespace AutoClicker.ViewModels
{
    public class LiteViewModel : ListenerViewModel
    {
        public LiteViewModel()
        {
            SelectedClickType = ClickTypes[0];

            SetStart();
        }

        protected override async void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (PickingInProgress)
            {
                PickingInProgress = false;
                return;
            }

            if (CanAccessExecution(e.KeyPressed))
            {
                await AsyncExecution();
            }
        }

        public uint Duration { get; set; } = 1000;
        public uint ClicksPerSecond { get; set; } = 100;
        public uint Delay { get; set; }

        private int _xCoordinate;
        public int XCoordinate
        {
            get => _xCoordinate;
            set
            {
                _xCoordinate = value;
                OnPropertyChanged();
            }
        }
        private int _yCoordinate;
        public int YCoordinate
        {
            get => _yCoordinate;
            set
            {
                _yCoordinate = value;
                OnPropertyChanged();
            }
        }

        private string _startButtonContent;
        public string StartButtonContent
        {
            get => _startButtonContent;
            set
            {
                _startButtonContent = value;
                OnPropertyChanged();
            }
        }

        private string _startButtonBackGround;
        public string StartButtonBackGround
        {
            get => _startButtonBackGround;
            set
            {
                _startButtonBackGround = value;
                OnPropertyChanged();
            }
        }
        private void SetContent(string content, string backgroundColor, bool actionInProgress)
        {
            StartButtonContent = content;
            StartButtonBackGround = backgroundColor;
            ActionInProgress = actionInProgress;
        }
        private void SetStart()
        {
            SetContent($"Start ({MySettings.Settings.StartButton})",
                "#FF59F109", false);
        }

        private void SetStop()
        {
            SetContent($"Stop ({MySettings.Settings.StopButton})",
                "#FFF71919", true);
        }


        public ObservableCollection<string> ClickTypes { get; } =
            new()
            {
                "Left", "Right", "Middle"
            };

        public string SelectedClickType { get; set; }


        private const uint MinClicksPerSecond = 1;
        private const uint MaxClicksPerSecond = 100;
        public bool TryParse(out AutoClickerInfo task)
        {
            if (ClicksPerSecond > MaxClicksPerSecond ||
                ClicksPerSecond < MinClicksPerSecond)
            {
                MessageBox.Show($"Incorrect clicks per second input");
                task = null;
                return false;
            }
            task = new AutoClickerInfo(
                TimeSpan.FromMilliseconds(Duration),
                ClicksPerSecond,
                TimeSpan.FromMilliseconds(Delay),
                (Clicks)Enum.Parse(typeof(Clicks), SelectedClickType),
                _userCursorActive,
                new MouseClicks.MousePoint(_xCoordinate, _yCoordinate));
            return true;
        }
        protected override async Task AsyncExecution()
        {
            if (!ActionInProgress)
            {
                if (!TryParse(out AutoClickerInfo task))
                    return;

                SetStop();

                await Task.Run(() => Execution(task));
            }
            SetStart();
        }

        private void Execution(AutoClickerInfo task)
        {
            var startPos = MouseClicks.GetCursorPosition();
            TimeSpan sleepTime = task.SleepTime();
            uint cycles = task.Cycles();
            Thread.Sleep(task.StartDelay);
            for (uint i = 0; i < cycles; i++)
            {
                if (!ActionInProgress)
                    break;
                if (!task.UserCursorActive)
                    MouseClicks.SetCursorPosition(task.Point);
                Thread.Sleep(1);
                MouseActions.MouseClick(task.ClickType);
                Thread.Sleep(sleepTime);
            }
            if (!task.UserCursorActive)
                MouseClicks.SetCursorPosition(startPos);
        }

        private bool _userCursorActive;
        public bool UserCursorActive
        {
            get => _userCursorActive;
            set
            {
                _userCursorActive = value;
                OnPropertyChanged(nameof(CoordinatesActive));
            }
        }
        public bool CoordinatesActive
        {
            get => !_userCursorActive;
            set => _userCursorActive = !value;
        }

        protected override void PickingCoordinates()
        {
            while (PickingInProgress)
            {
                var currentCoordinates =
                    MouseClicks.GetCursorPosition();
                XCoordinate = currentCoordinates.X;
                YCoordinate = currentCoordinates.Y;
                Thread.Sleep(1);
            }
        }
    }
}
