namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddServiceDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        services.AddScoped<IPaginationUriProvider, PaginationUriProvider>();
        services.AddHttpContextAccessor();
    }
}