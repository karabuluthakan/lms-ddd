namespace Domain.Entities.Students.Rules;

public class StudentIdRule : IBusinessRule
{
    private readonly string? _value;

    public StudentIdRule(string? value)
    {
        _value = value?.Trim();
    }

    public bool IsBroken()
    {
        if (string.IsNullOrEmpty(_value))
        {
            DetailMessage = "Student id is not null or empty";
            return true;
        }

        if (!Guid.TryParse(_value, out _))
        {
            DetailMessage = "Student id is incorrect";
        }

        return false;
    }

    public string? DetailMessage { get; private set; }
}