namespace Domain.Entities.Courses.Rules;

public class CourseNameRule : IBusinessRule
{
    private readonly string? _value;

    public CourseNameRule(string? value)
    {
        _value = value?.Trim();
    }

    public bool IsBroken()
    {
        if (string.IsNullOrEmpty(_value))
        {
            DetailMessage = "Course name is not null or empty";
            return true;
        }

        return false;
    }

    public string? DetailMessage { get; private set; }
}