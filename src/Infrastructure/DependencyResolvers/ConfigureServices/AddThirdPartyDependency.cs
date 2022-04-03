namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddThirdPartyDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        services.AddSingleton(_ =>
            new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); })
                .CreateMapper());
        services.AddSingleton<IMapperAdapter, AutoMapperAdapter>();

        services.AddSingleton<ICacheProvider, RedisCacheProvider>();

        services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
    }
}