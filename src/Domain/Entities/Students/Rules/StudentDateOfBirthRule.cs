namespace Domain.Entities.Students.Rules;

public class StudentDateOfBirthRule : IBusinessRule
{
    private const int _minimumAge = 15;
    private const int _maximumAge = 70;
    private readonly DateTime _value;
    private readonly DateTime _expectedMinimumDate = DateTime.UtcNow.AddYears(-_minimumAge);
    private readonly DateTime _expectedMaximumAge = DateTime.UtcNow.AddYears(-_maximumAge);

    public StudentDateOfBirthRule(DateTime value)
    {
        _value = value;
    }

    public bool IsBroken()
    {
        if (_value.Year < _expectedMinimumDate.Year)
        {
            DetailMessage = $"Student age greater than equal {_minimumAge}";
            return true;
        }

        if (_value.Year > _expectedMaximumAge.Year)
        {
            DetailMessage = $"Student age less than equal {_maximumAge}";
            return true;
        }

        return true;
    }

    public string? DetailMessage { get; private set; }
}