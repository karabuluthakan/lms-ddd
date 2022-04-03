namespace Infrastructure.DataAccess.EntityFramework.Context;

public class LmsDbContextFactory : IDbContextFactory<LmsDbContext>, IDisposable
{
    private readonly DbContextOptions<LmsDbContext> _options;
    private bool _disposed;

    public LmsDbContextFactory(DbContextOptions<LmsDbContext> options)
    {
        _options = Guard.Against.Null(options);
    }

    public LmsDbContext CreateDbContext()
    {
        return new LmsDbContext(_options);
    }

    ~LmsDbContextFactory()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed || !disposing)
        {
            return;
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}