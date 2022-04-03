namespace Application.Authentication.Models;

public class AccessTokenRequestDto : IDto
{
    public string Key { get; set; }
    public string Secret { get; set; }
    public GrantType Type { get; set; }
}