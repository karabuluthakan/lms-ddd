namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddHealthCheckerDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        services.AddScoped<ICacheHealthChecker, RedisCacheHealthChecker>();
        services.AddScoped<IDatabaseHealthChecker, PostgreSqlDatabaseHealthChecker>();
    }
}