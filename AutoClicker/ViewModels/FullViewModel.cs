using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoClicker.WorkWithDll;
using AutoClicker.WorkWithDll.InputSimulation;
using AutoClicker.WorkWithDll.Listener;

namespace AutoClicker.ViewModels
{
    public class FullViewModel : ListenerViewModel
    {
        private string _textBoxInput = "";
        public string TextBoxInput
        {
            get => _textBoxInput;
            set
            {
                _textBoxInput = value;
                OnPropertyChanged();
            }
        }
        public FullViewModel()
        {

        }
        protected override async Task AsyncExecution()
        {
            await Task.Run(() => Execution());
        }
        private static void Execution()
        {
            //just an example
            MouseCursor.SetCursorPosition(new MousePoint(300, 2100));
            SimulateInput.MouseEvent(MouseMessages.Left);
            Thread.Sleep(10);
            MouseCursor.SetCursorPosition(new MousePoint(300, 100));
            SimulateInput.MouseEvent(MouseMessages.Left);
            const string link = "https://github.com/kysect/AutoClicker/blob/additions/AutoClicker/ViewModels/ListenerViewModel.cs";
            foreach (var c in link)
            {
                SimulateInput.KeyboardEvent(c);
                Thread.Sleep(1);
            }
            SimulateInput.KeyboardEvent(Key.Enter);
        }

        protected override async void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (!e.KeyDown)
                return;

            if (CanAccessExecution(e.KeyPressed))
            {
                await AsyncExecution();
            }
        }

        protected override void ListenerOnMouseMessage(object sender, MouseArgs e)
        {

        }

        protected override void ListenerOnMouseMoved(object sender, EventArgs e)
        {

        }
    }
}
