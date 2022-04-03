namespace Infrastructure.CrossCuttingConcerns.Logging;

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }

    public ValueTask LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
        return ValueTask.CompletedTask;
    }

    public ValueTask LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
        return ValueTask.CompletedTask;
    }
}