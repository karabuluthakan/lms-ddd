#pragma warning disable CS8618
namespace Application.Authentication.Models;

public class AccessTokenResponse : IDto
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public AccessTokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = Guard.Against.NullOrEmpty(accessToken);
        RefreshToken = Guard.Against.NullOrEmpty(refreshToken);
    }

    public AccessTokenResponse()
    {
    }
}