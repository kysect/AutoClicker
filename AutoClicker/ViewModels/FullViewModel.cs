using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using AutoClicker.Commands;
using AutoClicker.Information;
using AutoClicker.Information.FullInfo;
using AutoClicker.Information.FullInfo.Actions;
using AutoClicker.WorkWithDll;
using AutoClicker.WorkWithDll.InputSimulation;
using AutoClicker.WorkWithDll.Listener;

namespace AutoClicker.ViewModels
{
    public class FullViewModel : ListenerViewModel
    {
        private bool _recordingInProgress;
        private DateTime _lastActionTime;

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
            StartRecording = new BaseCommand(OnStartRecording);
        }
        protected override async Task AsyncExecution()
        {
            if (!ActionInProgress)
            {
                if (!ActionParser.TryParse(TextBoxInput, out List<IAction> actionList))
                {
                    MessageBox.Show("Wrong input!");
                    return;
                }

                ActionInProgress = true;

                await Task.Run(() => Execution(actionList));
            }
            ActionInProgress = false;
        }
        
        private void Execution(List<IAction> actionList)
        {
            foreach (var action in actionList)
            {
                action.Execute();
                if (!ActionInProgress)
                    return;
            }
        }
        
        private void RecordSingleAction(ActionType action, string info)
        {
            TextBoxInput += $"{action}({info});\n";
        }
        private void RecordAction(ActionType action, string info)
        {
            DateTime currentTime = DateTime.Now;
            int millisecondsBetweenActions = (currentTime - _lastActionTime).Milliseconds;
            if (millisecondsBetweenActions != 0)
            {
                RecordSingleAction(ActionType.Sleep, millisecondsBetweenActions.ToString());
            }
            
            RecordSingleAction(action, info);
            _lastActionTime = currentTime;
        }
        protected override async void ListenerOnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (_recordingInProgress)
            {
                RecordAction(ActionType.Key, e.KeyPressed + ", " + (e.KeyDown ? "down" : "up"));
            }

            if (!e.KeyDown)
                return;

            if (CanAccessExecution(e.KeyPressed))
            {
                await AsyncExecution();
            }
        }

        protected override void ListenerOnMouseMessage(object sender, MouseArgs e)
        {
            if (_recordingInProgress)
            {
                RecordAction(ActionType.Mouse, e.Message.ToString());
            }
        }

        protected override void ListenerOnMouseMoved(object sender, EventArgs e)
        {
            if (_recordingInProgress)
            {
                MousePoint currentPosition = MouseCursor.GetCursorPosition();
                RecordAction(ActionType.Move, currentPosition.X + ", " + currentPosition.Y);
            }
        }

        public ICommand StartRecording { get; }

        private void OnStartRecording(object obj)
        {
            _recordingInProgress = !_recordingInProgress;
            _lastActionTime = DateTime.Now;
        }
    }
}
