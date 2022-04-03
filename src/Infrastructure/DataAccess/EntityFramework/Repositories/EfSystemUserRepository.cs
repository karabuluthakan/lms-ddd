namespace Infrastructure.DataAccess.EntityFramework.Repositories;

public class EfSystemUserRepository : EfBaseRepository<SystemUser,Guid>, ISystemUserRepository
{
    public EfSystemUserRepository(IDbContextFactory<LmsDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }
}