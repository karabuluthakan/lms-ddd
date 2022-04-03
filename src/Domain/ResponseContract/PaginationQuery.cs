namespace Domain.ResponseContract;

[Serializable]
public class PaginationQuery : IPaginationQuery
{
    private const int defaultPageNumber = 1;
    private const int defaultPageSize = 10;
    [JsonPropertyName("pageNumber")] public int PageNumber { get; }
    [JsonPropertyName("pageSize")] public int PageSize { get; }

    [JsonConstructor]
    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < defaultPageNumber ? defaultPageNumber : pageNumber;
        PageSize = pageSize < defaultPageSize ? defaultPageSize : pageSize;
    }

    [JsonConstructor]
    public PaginationQuery()
    {
        PageNumber = defaultPageNumber;
        PageSize = defaultPageSize;
    }
}