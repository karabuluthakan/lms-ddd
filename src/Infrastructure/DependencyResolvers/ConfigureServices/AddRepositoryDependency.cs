using Infrastructure.DataAccess.EntityFramework.Repositories;

namespace Infrastructure.DependencyResolvers.ConfigureServices;

public class AddRepositoryDependency : IConfigureServiceModule
{
    public void Load(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(EfBaseRepository<,>));
        services.AddScoped<ICourseRepository, EfCourseRepository>();
        services.AddScoped<IStudentRepository, EfStudentRepository>();
        services.AddScoped<ISystemUserRepository, EfSystemUserRepository>();
    }
}