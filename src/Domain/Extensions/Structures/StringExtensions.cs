#pragma warning disable CS8603

namespace Domain.Extensions.Structures;

public static class StringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="TDest"></typeparam>
    /// <returns></returns>
    public static TDest FromJson<TDest>(this string source)
    {
        if (source is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<TDest>(source, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        });
    }
}