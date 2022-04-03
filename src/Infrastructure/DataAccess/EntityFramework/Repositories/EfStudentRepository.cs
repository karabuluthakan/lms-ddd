namespace Infrastructure.DataAccess.EntityFramework.Repositories;

public class EfStudentRepository : EfBaseRepository<Student, Guid>, IStudentRepository
{
    public EfStudentRepository(IDbContextFactory<LmsDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }

    public async ValueTask<IReadOnlyList<Course>> GetCourses(Guid studentId)
    {
        throw new NotImplementedException();
    }
}