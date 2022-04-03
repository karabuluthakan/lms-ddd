namespace Domain.ResponseContract.Abstract;

public interface IPaginationQuery
{
    int PageNumber { get; }
    int PageSize { get; }
}