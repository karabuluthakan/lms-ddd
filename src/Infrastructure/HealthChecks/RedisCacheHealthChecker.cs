namespace Infrastructure.HealthChecks;

/// <summary>
/// 
/// </summary>
public class RedisCacheHealthChecker : ICacheHealthChecker
{
    private readonly ICacheProvider _cache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    public RedisCacheHealthChecker(ICacheProvider cache)
    {
        _cache = Guard.Against.Null(cache);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask<IResponse> CheckStatus()
    {
        if (await _cache.CheckStatus())
        {
            return Response.OK("Healthy");
        }

        return Response.BadRequest("Unhealthy");
    }
}