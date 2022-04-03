namespace Domain.Entities.Courses.DataTransferObjects;

public record UpsertCourseDto : IDto
{
    public string Name { get; init; }
    public string CoursePrefix { get; init; }
}