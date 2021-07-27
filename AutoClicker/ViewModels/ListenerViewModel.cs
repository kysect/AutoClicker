using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AutoClicker.Commands;
using AutoClicker.Information;
using AutoClicker.WorkWithDll;

namespace AutoClicker.ViewModels
{
    public abstract class ListenerViewModel : MainViewModel
    {
        protected bool ActionInProgress;
        protected bool PickingInProgress;

        private LowLevelKeyboardListener _listener;

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

        public void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += ListenerOnKeyPressed;
            _listener.HookKeyboard();
        }

        protected bool CanAccessExecution(Key pressedKey)
        {
            if (ActionInProgress)
                return pressedKey == MySettings.Settings.StopButton;
            return pressedKey == MySettings.Settings.StartButton;
        }
        protected abstract void ListenerOnKeyPressed(object sender, KeyPressedArgs e);

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

        protected async void OnStartPicking(object obj)
        {
            PickingInProgress = true;
            await Task.Run(() => PickingCoordinates());
        }

        protected abstract void PickingCoordinates();
    }
}
