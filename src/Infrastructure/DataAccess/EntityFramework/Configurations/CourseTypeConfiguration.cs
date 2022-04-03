namespace Infrastructure.DataAccess.EntityFramework.Configurations;

public class CourseTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(DbConstants.CourseTable);
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CoursePrefix).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnType(DbConstants.GuidType)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt)
            .HasColumnType(DbConstants.DateTimeOffsetType)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnType(DbConstants.VarcharType)
            .HasMaxLength(DbConstants.DefaultStringMaxLength)
            .IsRequired();

        builder.Property(x => x.CoursePrefix)
            .HasColumnType(DbConstants.VarcharType)
            .HasMaxLength(10)
            .IsRequired();
    }
}