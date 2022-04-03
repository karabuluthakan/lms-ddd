#pragma warning disable CS8618

namespace Infrastructure.Providers;

public class PaginationUriProvider : IPaginationUriProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PaginationUriProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = Guard.Against.Null(httpContextAccessor);
    }

    public Uri GenerateUri(IPaginationQuery query)
    {
        var request = Guard.Against.Null(_httpContextAccessor.HttpContext!.Request);
        var baseUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        var route = request.Path.Value;
        var endpoint = new Uri(string.Concat(baseUri, route));
        var queryUri = QueryHelpers.AddQueryString($"{endpoint}", "pageNumber", $"{query.PageNumber}");
        queryUri = QueryHelpers.AddQueryString(queryUri, "pageSize", $"{query.PageSize}");
        return new Uri(queryUri);
    }
}