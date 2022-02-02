﻿using System;
using System.Windows.Input;

namespace AutoClicker.Desktop.Commands;

public class BaseCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;
    
    public event EventHandler CanExecuteChanged;

    public BaseCommand(Action<object> execute, Func<object, bool> canExecute = null) =>
        (_execute, _canExecute) = (execute, canExecute);

    public bool CanExecute(object parameter) =>
        _canExecute?.Invoke(parameter) ?? true;

    public void Execute(object parameter) =>
        _execute(parameter);
}

