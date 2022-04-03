namespace Domain.SharedCore.Abstract;

/// <summary>
/// 
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    string GenerateAccessToken(List<Claim> claims);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    string GeneratePasswordResetToken(List<Claim> claims);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    string GenerateRefreshToken(Guid userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> ValidateRefreshToken(string refreshToken, Guid userId);
}