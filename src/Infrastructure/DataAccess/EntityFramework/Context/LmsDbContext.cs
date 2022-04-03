#pragma warning disable CS8618

namespace Infrastructure.DataAccess.EntityFramework.Context;

public class LmsDbContext : DbContext
{
    public LmsDbContext(DbContextOptions<LmsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DbConstants.DefaultScheme);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LmsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<SystemUser> Users { get; set; }
}