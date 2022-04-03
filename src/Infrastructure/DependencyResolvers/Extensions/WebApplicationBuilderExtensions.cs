namespace Infrastructure.DependencyResolvers.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void DependencyResolvers(this WebApplicationBuilder builder, Assembly[] assemblies)
    {
        builder = Guard.Against.Null(builder);
        assemblies = Guard.Against.Null(assemblies);
        var exportedTypes = assemblies.SelectMany(x => x.ExportedTypes).ToList();
        if (exportedTypes is null || !exportedTypes.Any())
        {
            return;
        }

        IEnumerable<Type> GetAssignableTypes(Type type)
        {
            return exportedTypes.Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        }

        var services = Guard.Against.Null(builder.Services);
        var serviceInstallers = GetAssignableTypes(typeof(IConfigureServiceModule))
            .Select(Activator.CreateInstance).Cast<IConfigureServiceModule>().ToList();
        serviceInstallers.ForEach(installer => installer.Load(services));

        var app = Guard.Against.Null(builder.Build());
        var appInstallers = GetAssignableTypes(typeof(IConfigureModule))
            .Select(Activator.CreateInstance).Cast<IConfigureModule>().OrderBy(x => x.Priority).ToList();
        appInstallers.ForEach(installer => installer.Load(app));
    }
}