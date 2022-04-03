namespace Domain.ResponseContract;

/// <summary>
/// 
/// </summary>
[Serializable]
public class Response : IResponse
{
    [JsonPropertyName("statusCode")] public HttpStatusCode StatusCode { get; }
    [JsonPropertyName("message")] public string Message { get; }
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonConstructor]
    protected Response(HttpStatusCode statusCode, string? message = "")
    {
        StatusCode = statusCode;
        Success = (int)statusCode < 305;
        Message = string.IsNullOrEmpty(message?.Trim()) ? statusCode.ToString() : message.Trim();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response OK(string? message = "")
    {
        return new Response(HttpStatusCode.OK, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response Created(string? message = "")
    {
        return new Response(HttpStatusCode.Created, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response NoContent(string? message = "")
    {
        return new Response(HttpStatusCode.NoContent, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response BadRequest(string? message = "")
    {
        return new Response(HttpStatusCode.BadRequest, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response Unauthorized(string? message = "")
    {
        return new Response(HttpStatusCode.Unauthorized, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response Forbidden(string? message = "")
    {
        return new Response(HttpStatusCode.Forbidden, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response NotFound(string? message = "")
    {
        return new Response(HttpStatusCode.NotFound, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response InternalServerError(string? message = "")
    {
        return new Response(HttpStatusCode.InternalServerError, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response NotImplemented(string? message = "")
    {
        return new Response(HttpStatusCode.NotImplemented, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static Response BadGateway(string? message = "")
    {
        return new Response(HttpStatusCode.BadGateway, message);
    }
}