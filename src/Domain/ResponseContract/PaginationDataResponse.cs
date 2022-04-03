namespace Domain.ResponseContract;

[Serializable]
public class PaginationDataResponse<T> : IPaginationResponse<T> where T : class, IDto, new()
{
    [JsonPropertyName("data")] public IReadOnlyList<T> Data { get; }
    [JsonPropertyName("pageNumber")] public int PageNumber { get; }
    [JsonPropertyName("pageSize")] public int PageSize { get; }
    [JsonPropertyName("totalPages")] public int TotalPages { get; }
    [JsonPropertyName("totalRecords")] public int TotalRecords { get; }
    [JsonPropertyName("firstPage")] public Uri FirstPage { get; }
    [JsonPropertyName("lastPage")] public Uri LastPage { get; }
    [JsonPropertyName("nextPage")] public Uri? NextPage { get; }
    [JsonPropertyName("previousPage")] public Uri? PreviousPage { get; }
    [JsonPropertyName("statusCode")] public HttpStatusCode StatusCode { get; }
    [JsonPropertyName("message")] public string Message { get; }
    [JsonPropertyName("success")] public bool Success { get; set; }

    private readonly IPaginationUriProvider _uriProvider;

    [JsonConstructor]
    public PaginationDataResponse(IReadOnlyList<T> data, IPaginationQuery query, int totalRecords,
        IPaginationUriProvider uriProvider, string? message = "")
    {
        _uriProvider = Guard.Against.Null(uriProvider);
        var totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)query.PageSize));
        Data = Guard.Against.Null(data);
        PageNumber = query.PageNumber;
        PageSize = query.PageSize;
        NextPage = query.PageNumber >= 1 && query.PageNumber < totalPages
            ? _uriProvider.GenerateUri(new PaginationQuery(query.PageNumber + 1, query.PageSize))
            : null;
        PreviousPage = query.PageNumber - 1 >= 1 && query.PageNumber <= totalPages
            ? _uriProvider.GenerateUri(new PaginationQuery(query.PageNumber - 1, query.PageSize))
            : null;
        FirstPage = _uriProvider.GenerateUri(new PaginationQuery(1, query.PageSize));
        LastPage = _uriProvider.GenerateUri(new PaginationQuery(totalPages, query.PageSize));
        StatusCode = HttpStatusCode.OK;
        Message = string.IsNullOrEmpty(message?.Trim()) ? "OK" : message.Trim();
        Success = true;
    }

    public static PaginationDataResponse<T> OK(IReadOnlyList<T> data, IPaginationQuery query, int totalRecords,
        IPaginationUriProvider uriProvider, string? message = "")
    {
        return new PaginationDataResponse<T>(data, query, totalRecords, uriProvider, message);
    }
}