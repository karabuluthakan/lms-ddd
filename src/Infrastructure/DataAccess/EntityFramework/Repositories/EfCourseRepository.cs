namespace Infrastructure.DataAccess.EntityFramework.Repositories;

public class EfCourseRepository : EfBaseRepository<Course,Guid>, ICourseRepository
{
    public EfCourseRepository(IDbContextFactory<LmsDbContext> dbContextFactory)
        : base(dbContextFactory)
    {
    }
}