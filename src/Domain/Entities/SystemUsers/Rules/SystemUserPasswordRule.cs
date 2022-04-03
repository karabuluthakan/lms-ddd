namespace Domain.Entities.SystemUsers.Rules;

public class SystemUserPasswordRule : IBusinessRule
{
    private readonly string? _value;
    private StringBuilder _message;

    public SystemUserPasswordRule(string? value)
    {
        _value = value;
        _message = new StringBuilder();
    }

    public bool IsBroken()
    {
        if (string.IsNullOrEmpty(_value))
        {
            _message.Append("Password is not null or empty");
            return true;
        }

        var hasNumber = new Regex(@"[0-9]+");
        if (!hasNumber.IsMatch(_value))
        {
            _message.Append("Password should be contain alpha numeric character");
            return true;
        }

        var hasUpperChar = new Regex(@"[A-Z]+");
        if (!hasUpperChar.IsMatch(_value))
        {
            _message.Append("Password should be contain upper char");
            return true;
        }

        var hasMinimumChars = new Regex(@".{6,}");
        if (!hasMinimumChars.IsMatch(_value))
        {
            _message.Append("Password should be greater than equal 6 character");
            return true;
        }

        return false;
    }

    public string? DetailMessage => _message.ToString();
}