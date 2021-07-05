using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoClicker.Information;
using AutoClicker.WorkWithDll;

namespace AutoClicker.LiteVersion
{
    public partial class LiteWindow
    {
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
                MessageBox.Show("Incorrect clicks per second input");
                correctInput = false;
            }
            task.ClicksPerSecond = clicksPerSecond;

            if (!int.TryParse(DelayBox.Text, out int delay) ||
                delay < 0)
            {
                MessageBox.Show("Incorrect start delay input");
                correctInput = false;
            }
            task.StartDelay = TimeSpan.FromMilliseconds(delay);

            if (!Enum.TryParse(((TextBlock)((ComboBoxItem)ClicksType.SelectedItem).Content).Text,
                out Clicks clickType))
            {
                MessageBox.Show("Incorrect click type input");
                correctInput = false;
            }
            task.ClickType = clickType;

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
    }
}
