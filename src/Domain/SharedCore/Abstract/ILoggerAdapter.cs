namespace Domain.SharedCore.Abstract;

public interface ILoggerAdapter<T>
{
    ValueTask LogInformation(string message, params object[] args);
    ValueTask LogWarning(string message, params object[] args);
}