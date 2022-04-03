namespace Domain.Entities.Courses.DataTransferObjects;

public record AddStudentForCourseDto : IDto
{
    public string Id { get; init; }
    // public List<> Students { get; set; }
}