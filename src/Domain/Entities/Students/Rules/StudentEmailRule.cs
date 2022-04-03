namespace Domain.Entities.Students.Rules;

public class StudentEmailRule : IBusinessRule
{
    private readonly string? _value;

    public StudentEmailRule(string? value)
    {
        _value = value;
    }

    public bool IsBroken()
    {
        if (string.IsNullOrEmpty(_value))
        {
            DetailMessage = "Email is not null or empty";
            return true;
        }

        if (!IsValidEmail(_value))
        {
            DetailMessage = "Email is invalid";
            return true;
        }

        return false;
    }

    private bool IsValidEmail(string mail)
    {
        var trim = mail.Trim().ToLowerInvariant();
        if (trim.EndsWith("."))
        {
            return false;
        }

        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(trim);
            return mailAddress.Address.Equals(mail);
        }
        catch
        {
            return false;
        }
    }

    public string? DetailMessage { get; private set; }
}