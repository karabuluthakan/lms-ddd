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
    public string TokenPrefix { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Security { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string DefaultScheme { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TokenSettingItem SystemUser { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TokenSettingItem DefaultUser { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int AccessTokenExpiration { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int RefreshTokenExpiration { get; set; }
}

/// <summary>
/// 
/// </summary>
public class TokenSettingItem
{
    /// <summary>
    /// 
    /// </summary>
    public string Scheme { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string MetadataAddress { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Authority { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] ValidAudiences { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] ValidIssuers { get; set; }
}