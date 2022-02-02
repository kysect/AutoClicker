using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoClicker.Common.Tools;

public abstract class Notifier : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void Update<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return;
    
        field = value;
        OnPropertyChanged(propertyName);
    }

    protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
