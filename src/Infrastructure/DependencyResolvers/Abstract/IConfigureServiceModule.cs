namespace Infrastructure.DependencyResolvers.Abstract;

/// <summary>
/// 
/// </summary>
public interface IConfigureServiceModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    void Load(IServiceCollection services);
}