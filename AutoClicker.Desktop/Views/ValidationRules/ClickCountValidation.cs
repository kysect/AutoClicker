using System.Globalization;
using System.Windows.Controls;

namespace AutoClicker.Desktop.Views.ValidationRules;

public class ClickCountValidation : ValidationRule
{
    private const int MinClicksPerSecond = 1;
    private const int MaxClicksPerSecond = 100;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is null || !int.TryParse(value.ToString(), out var clicksPerSecond))
            return new ValidationResult(false, "Field is not a number");

        return clicksPerSecond switch
        {
            < MinClicksPerSecond => new ValidationResult(false, 
                $"Minimum number of clicks per second is {MinClicksPerSecond}"),

            > MaxClicksPerSecond => new ValidationResult(false,
                $"Maximum number of clicks per second is {MaxClicksPerSecond}"),

            _ => ValidationResult.ValidResult
        };
    }
}