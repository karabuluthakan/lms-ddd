namespace Domain.ResponseContract.Abstract;

/// <summary>
/// 
/// </summary>
public interface IPaginationUriProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Uri GenerateUri(IPaginationQuery query);
}