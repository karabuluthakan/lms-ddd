namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddDbContextDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        var connectionString = configuration.GetConnectionString(DbConstants.DbContextSection);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(DbConstants.DbContextSection));
        }

        services.AddPooledDbContextFactory<LmsDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString,
                builder =>
                {
                    builder.CommandTimeout(DbConstants.CommandTimeout);
                    builder.MigrationsAssembly(DbConstants.MigrationsAssembly);
                }).UseCamelCaseNamingConvention();
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}