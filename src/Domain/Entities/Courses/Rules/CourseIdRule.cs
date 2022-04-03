namespace Domain.Entities.Courses.Rules;

public class CoursePrefixRule : IBusinessRule
{
    public string? DetailMessage { get; private set; }

    private readonly string? _value;

    public CoursePrefixRule(string? value)
    {
        _value = value;
    }

    public bool IsBroken()
    {
        if (string.IsNullOrEmpty(_value?.Trim()))
        {
            DetailMessage = "Course Id is not null or empty";
            return true;
        }

        //Todo: Other check rule will implement

        return false;
    }
}