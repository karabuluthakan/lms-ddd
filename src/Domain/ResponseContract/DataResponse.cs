namespace Domain.ResponseContract;

[Serializable]
public class DataResponse : IDataResponse
{
    [JsonPropertyName("data")] public IDto? Data { get; }
    [JsonPropertyName("statusCode")] public HttpStatusCode StatusCode { get; }
    [JsonPropertyName("message")] public string Message { get; }
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonConstructor]
    protected DataResponse(IDto data, string? message)
    {
        Data = data;
        Message = string.IsNullOrEmpty(message?.Trim()) ? StatusCode.ToString() : message.Trim();
        StatusCode = data is null ? HttpStatusCode.NotFound : HttpStatusCode.OK;
        Success = data is not null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static DataResponse Get(IDto data, string? message = "")
    {
        return new DataResponse(data, message);
    }
}