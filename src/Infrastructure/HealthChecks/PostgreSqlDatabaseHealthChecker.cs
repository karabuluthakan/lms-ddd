namespace Infrastructure.HealthChecks;

public class PostgreSqlDatabaseHealthChecker : IDatabaseHealthChecker
{
    private readonly IDbContextFactory<LmsDbContext> _dbContextFactory;


    public PostgreSqlDatabaseHealthChecker(IDbContextFactory<LmsDbContext> dbContextFactory)
    {
        _dbContextFactory = Guard.Against.Null(dbContextFactory);
    }

    public async ValueTask<IResponse> CheckStatus()
    {
        await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                return Response.OK("Healthy");
            }
            return Response.BadRequest("Unhealthy");
        }
    }
}