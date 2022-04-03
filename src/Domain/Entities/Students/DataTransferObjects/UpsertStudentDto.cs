#pragma warning disable CS8618
namespace Domain.Entities.Students.DataTransferObjects;

public record UpsertStudentDto : IDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public DateTime BirthDate { get; init; }
}