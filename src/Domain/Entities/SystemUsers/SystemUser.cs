#pragma warning disable CS8618
namespace Domain.Entities.SystemUsers;

public class SystemUser : Entity
{
    public string Email { get; }
    public string PasswordHash { get; }

    public SystemUser(string email, string plainPassword)
    {
        CheckRule(new SystemUserEmailRule(email));
        CheckRule(new SystemUserPasswordRule(plainPassword));
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = CryptoHandler.GeneratePassword(plainPassword);
    }

    public SystemUser()
    {
    }
}