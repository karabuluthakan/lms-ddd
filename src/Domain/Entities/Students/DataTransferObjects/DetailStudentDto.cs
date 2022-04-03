#pragma warning disable CS8618

namespace Domain.Entities.Students.DataTransferObjects;

public class DetailStudentDto : IDto
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public DateOnly BirthDate { get; init; }
    public List<DetailCourseDto> Courses { get; init; }
}