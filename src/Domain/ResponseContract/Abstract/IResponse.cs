namespace Domain.ResponseContract.Abstract;

public interface IResponse
{
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
    public bool Success { get; set; }
}