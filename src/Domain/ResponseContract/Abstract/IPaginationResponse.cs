namespace Domain.ResponseContract.Abstract;

public interface IPaginationResponse<out T> : IResponse where T : class, IDto, new()
{
    IReadOnlyList<T> Data { get; }
    int PageNumber { get; }
    int PageSize { get; }
    int TotalPages { get; }
    int TotalRecords { get; }
    Uri FirstPage { get; }
    Uri LastPage { get; }
    Uri? NextPage { get; }
    Uri? PreviousPage { get; }
}