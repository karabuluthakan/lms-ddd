namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddSettingsDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.Configure<CacheSettings>(opt => configuration.GetSection(nameof(CacheSettings)).Bind(opt));
        services.Configure<TokenSettings>(opt => configuration.GetSection(nameof(TokenSettings)).Bind(opt));
    }
}