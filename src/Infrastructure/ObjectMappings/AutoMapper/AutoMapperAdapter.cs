#pragma warning disable CS8603
namespace Infrastructure.ObjectMappings.AutoMapper;

/// <summary>
/// 
/// </summary>
public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    public AutoMapperAdapter(IMapper mapper)
    {
        _mapper = Guard.Against.Null(mapper);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TDestination"></typeparam>
    /// <returns></returns>
    public TDestination Map<TDestination>(object source)
    {
        if (source is null)
        {
            return default(TDestination);
        }

        return _mapper.Map<TDestination>(source);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <returns></returns>
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source is null)
        {
            return destination;
        }

        return _mapper.Map(source, destination);
    }
}