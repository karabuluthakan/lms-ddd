namespace Domain.DataAccess;

public interface IStudentRepository : IRepository<Student,Guid>
{
    ValueTask<IReadOnlyList<Course>> GetCourses(Guid studentId);
}