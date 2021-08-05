using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoClicker.Commands;
using AutoClicker.Information;
using AutoClicker.WorkWithDll.Listener;

namespace AutoClicker.ViewModels
{
    public abstract class ListenerViewModel : MainViewModel
    {
        protected bool ActionInProgress;
        protected bool PickingInProgress;

        private LowLevelListener _listener;

        protected ListenerViewModel()
        {
            StartExecution = new BaseCommand(OnStartExecution);
            StartPicking = new BaseCommand(OnStartPicking);
        }
        
        protected override void OnOpenWindow(object obj)
        {
            ActionInProgress = false;
            PickingInProgress = false;
            base.OnOpenWindow(obj);
        }

        protected bool CanAccessExecution(Key pressedKey)
        {
            if (ActionInProgress)
                return pressedKey == MySettings.Settings.StopButton;
            return pressedKey == MySettings.Settings.StartButton;
        }


        public void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _listener = new LowLevelListener();
            _listener.OnKeyPressed += ListenerOnKeyPressed;
            _listener.OnMouseMessage += ListenerOnMouseMessage;
            _listener.OnMouseMoved += ListenerOnMouseMoved;
            _listener.HookKeyboard();
        }

        protected abstract void ListenerOnKeyPressed(object sender, KeyPressedArgs e);
        protected abstract void ListenerOnMouseMessage(object sender, MouseArgs e);
        protected abstract void ListenerOnMouseMoved(object sender, EventArgs e);


        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            _listener.UnHookKeyboard();
        }


        public ICommand StartExecution { get; }

        protected async void OnStartExecution(object obj)
        {
            await AsyncExecution();
        }

        protected abstract Task AsyncExecution();


        public ICommand StartPicking { get; }

        protected void OnStartPicking(object obj)
        {
            PickingInProgress = true;
        }
    }
}
