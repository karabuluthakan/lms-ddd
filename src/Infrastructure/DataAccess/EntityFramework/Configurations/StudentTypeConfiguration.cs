namespace Infrastructure.DataAccess.EntityFramework.Configurations;

public class StudentTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(DbConstants.StudentTable);

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Id)
            .HasColumnType(DbConstants.GuidType)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt)
            .HasColumnType(DbConstants.DateTimeOffsetType)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.BirthDate)
            .HasColumnType(DbConstants.DateOnlyType)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasColumnType(DbConstants.VarcharType)
            .HasMaxLength(DbConstants.DefaultStringLength)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnType(DbConstants.VarcharType)
            .HasMaxLength(DbConstants.DefaultStringLength)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnType(DbConstants.VarcharType)
            .HasMaxLength(DbConstants.DefaultStringMaxLength)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnType(DbConstants.VarcharType)
            .IsRequired();
    }
}