namespace Domain.ResponseContract.Abstract;

/// <summary>
/// 
/// </summary>c
public interface IDataResponse : IResponse
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("data")]
    IDto? Data { get; }
}