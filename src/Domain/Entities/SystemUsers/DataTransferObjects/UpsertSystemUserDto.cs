namespace Domain.Entities.SystemUsers.DataTransferObjects;

public record UpsertSystemUserDto : IDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}
