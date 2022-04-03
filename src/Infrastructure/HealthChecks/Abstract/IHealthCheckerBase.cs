namespace Infrastructure.HealthChecks.Abstract;

public interface IHealthCheckerBase
{
    ValueTask<IResponse> CheckStatus();
}