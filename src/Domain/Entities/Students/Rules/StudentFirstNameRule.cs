namespace Domain.Entities.Students.Rules;

public class StudentNameRule : IBusinessRule
{
    private readonly string? _value;
    private readonly string _propertyName;

    public StudentNameRule(string? value, string propertyName)
    {
        _propertyName = propertyName;
        _value = value?.Trim();
    }

    public bool IsBroken()
    {
        if (string.IsNullOrEmpty(_value))
        {
            DetailMessage = $"Student {_propertyName} is not null or empty";
            return true;
        }

        if (_value.Length < 2)
        {
            DetailMessage = $"Student {_propertyName} is greater than equal 2 characters";
            return true;
        }

        if (!Regex.IsMatch(_value, @"^[a-zA-Z]+$"))
        {
            DetailMessage = $"Student {_propertyName} just only alphabetic characters";
            return true;
        }

        return false;
    }

    public string? DetailMessage { get; private set; }
}