#pragma warning disable CS8618
namespace Domain.SharedCore.AppSettings;

/// <summary>
/// 
/// </summary>
public class TokenSettings : IAppSettings
{
    /// <summary>
    /// 
    /// </summary>
    public string Security { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int AccessTokenExpiration { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int RefreshTokenExpiration { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PasswordResetMinutes { get; set; }
}