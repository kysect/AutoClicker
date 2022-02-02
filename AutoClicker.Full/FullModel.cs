using AutoClicker.Common.Tools;

namespace AutoClicker.Full;

public class FullModel : Notifier
{
    private string _textBoxInput = "";

    public string TextBoxInput
    {
        get => _textBoxInput;
        set => Update(ref _textBoxInput, value);
    }
}