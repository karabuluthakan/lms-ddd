#pragma warning disable CS8618
namespace Domain.Entities.Students.DataTransferObjects;

public record AddCourseForStudentDto : IDto
{
    public string Id { get; init; }
}