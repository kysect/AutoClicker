﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoClicker.WorkWithDll;
using AutoClicker.Information;
using AutoClicker.Execution;
namespace AutoClicker.LiteVersion
{
    public partial class LiteWindow : Window
    {
        private bool _actionInProgress;
        private bool _pickingInProgress;
        private LowLevelKeyboardListener _listener;

        public LiteWindow()
        {
            InitializeComponent();
            MySettings.Settings.LastOpenedWindow = Windows.Lite;
            SetStart();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += ListenerOnKeyPressed;
            _listener.HookKeyboard();
        }

        private void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (_pickingInProgress)
            {
                _pickingInProgress = false;
                return;
            }

            if ((_actionInProgress &&
                 e.KeyPressed == MySettings.Settings.StopButton) ||
                (!_actionInProgress &&
                 e.KeyPressed == MySettings.Settings.StartButton))
            {
                AsyncExecution();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _listener.UnHookKeyboard();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            AsyncExecution();
        }

        private void SetContent(string content, string backgroundColor, bool actionInProgress)
        {
            StartButton.Content = content;
            StartButton.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(backgroundColor);
            _actionInProgress = actionInProgress;
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
        private async void AsyncExecution()
        {
            if (!_actionInProgress)
            {
                bool correctInput = true;
                AutoClickerInfo task = DataParse(ref correctInput);
                if (!correctInput)
                    return;

                SetStop();

                await Task.Run(() => Execution(task));
            }
            SetStart();
        }

        private void Execution(AutoClickerInfo task)
        {
            var startPos = MouseClicks.GetCursorPosition();
            TimeSpan sleepTime = TimeSpan.FromMilliseconds(1000 / task.ClicksPerSecond - 3);
            int cycles = (int)(task.ClicksPerSecond * task.Duration.TotalSeconds);
            Thread.Sleep(task.StartDelay);
            for (int i = 0; i < cycles; i++)
            {
                if (!_actionInProgress)
                    break;
                if (!task.UserCursor)
                    MouseClicks.SetCursorPosition(task.Point);
                Thread.Sleep(1);
                MouseActions.MouseClick(task.ClickType);
                Thread.Sleep(sleepTime);
            }
            if (!task.UserCursor)
                MouseClicks.SetCursorPosition(startPos);
        }

        private void Cur_Checked(object sender, RoutedEventArgs e)
        {
            Coordinates.Visibility = Visibility.Hidden;
            PickButton.Visibility = Visibility.Hidden;
        }

        private void Cur_Unchecked(object sender, RoutedEventArgs e)
        {
            Coordinates.Visibility = Visibility.Visible;
            PickButton.Visibility = Visibility.Visible;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            _actionInProgress = false;
            _pickingInProgress = false;
            var menu = new MainWindow();
            menu.Show();
            Close();
        }

        private async void PickButton_Click(object sender, RoutedEventArgs e)
        {
            _pickingInProgress = true;
            await Task.Run(() => PickingCoordinates());
        }

        private void PickingCoordinates()
        {
            try
            {
                while (_pickingInProgress)
                {
                    var currentCoordinates =
                        MouseClicks.GetCursorPosition();
                    Dispatcher.Invoke(() => XTextBox.Text =
                        currentCoordinates.X.ToString());
                    Dispatcher.Invoke(() => YTextBox.Text =
                        currentCoordinates.Y.ToString());
                    Thread.Sleep(1);
                }
            }
            catch (TaskCanceledException)
            {
                _pickingInProgress = false;
            }
        }
    }
}