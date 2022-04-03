namespace Domain.SeedWork;

public interface IBusinessRule
{
    public bool IsBroken();
    public string? DetailMessage { get; }
}