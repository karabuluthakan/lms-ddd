namespace Infrastructure.DependencyResolvers.Abstract;

/// <summary>
/// 
/// </summary>
public interface IConfigureModule
{
    /// <summary>
    /// 
    /// </summary>
    int Priority { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    void Load(IApplicationBuilder app);
}