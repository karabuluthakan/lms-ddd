namespace Domain.Exceptions;

public class BusinessRuleException : Exception
{
    public BusinessRuleException(IBusinessRule rule, Exception innerException) 
        : base(rule.DetailMessage, innerException)
    {
    }

    public BusinessRuleException(IBusinessRule rule) : base(rule.DetailMessage)
    {
    }
}