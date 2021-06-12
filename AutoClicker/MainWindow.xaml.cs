using System;
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

namespace AutoClicker
{
    public partial class MainWindow : Window
    {
        private bool ActionInProgress = false;
        private LowLevelKeyboardListener Listener;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Listener = new LowLevelKeyboardListener();
            Listener.OnKeyPressed += ListenerOnKeyPressed;
            Listener.HookKeyboard();
        }

        void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (e.KeyPressed == Key.F9)
            {
                AsyncMouseClick();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Listener.UnHookKeyboard();
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            AsyncMouseClick();
        }

        public AutoClickerInfo DataParse(ref bool correctInput)
        {
            var task = new AutoClickerInfo();

            if (!int.TryParse(DurationTextBox.Text, out int duration) ||
                    duration < -1)
            {
                MessageBox.Show("Incorrect duration input");
                correctInput = false;
            }
            task.Duration = TimeSpan.FromMilliseconds(duration);

            if (!int.TryParse(ClicksTextBox.Text, out int clicksPerSecond) ||
                clicksPerSecond < 1 ||
                clicksPerSecond > 100)
            {
                MessageBox.Show("Incorrect c/s input");
                correctInput = false;
            }
            task.ClicksPerSecond = clicksPerSecond;

            task.UserCursor = (bool)CurBox.IsChecked;
            if (!task.UserCursor)
            {
                int y = 0;
                if (!int.TryParse(XTextBox.Text, out int x) ||
                   !int.TryParse(YTextBox.Text, out y) ||
                   x < 0 ||
                   y < 0)
                {
                    MessageBox.Show("Incorrect coordinates input");
                    correctInput = false;
                }
                task.Point = new MouseClicks.MousePoint(x, y);
            }
            return task;
        }

        private async void AsyncMouseClick()
        {
            if ((StartStop.Content).ToString().Contains("Start"))
            {
                bool correctInput = true;

                AutoClickerInfo task = DataParse(ref correctInput);

                if (!correctInput)
                    return;

                StartStop.Content = "Stop (F9)";
                StartStop.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF71919");
                ActionInProgress = true;
                await Task.Run(()
                    => 
                MouseClick(task));
            }
            else
            {
                ActionInProgress = false;
            }
            StartStop.Content = "Start (F9)";
            StartStop.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF59F109");
        }
        
        private void MouseClick(AutoClickerInfo task)
        {
            var startPos = MouseClicks.GetCursorPosition();
            TimeSpan sleepTime = TimeSpan.FromMilliseconds(1000 / task.ClicksPerSecond - 3);
            int cycles = (int)(task.ClicksPerSecond * task.Duration.TotalSeconds);
            for (int i = 0; i < cycles; i++)
            {
                if (!ActionInProgress)
                    break;
                if (!task.UserCursor)
                    MouseClicks.SetCursorPosition(task.Point);
                Thread.Sleep(1);
                MouseClicks.MouseEvent(MouseClicks.MouseEventFlags.LeftDown);
                MouseClicks.MouseEvent(MouseClicks.MouseEventFlags.LeftUp);
                Thread.Sleep(sleepTime);
            }
            if (!task.UserCursor)
                MouseClicks.SetCursorPosition(startPos);
        }

        private void Cur_Checked(object sender, RoutedEventArgs e)
        {
            Coordinates.Visibility = Visibility.Hidden;
        }

        private void Cur_Unchecked(object sender, RoutedEventArgs e)
        {
            Coordinates.Visibility = Visibility.Visible;
        }
    }
}
