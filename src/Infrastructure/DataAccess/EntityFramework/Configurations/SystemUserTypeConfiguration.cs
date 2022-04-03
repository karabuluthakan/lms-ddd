namespace Infrastructure.DataAccess.EntityFramework.Configurations;

public class SystemUserTypeConfiguration : IEntityTypeConfiguration<SystemUser>
{
    public void Configure(EntityTypeBuilder<SystemUser> builder)
    {
        builder.ToTable(DbConstants.SystemUserTable);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType(DbConstants.GuidType)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt)
            .HasColumnType(DbConstants.DateTimeOffsetType)
            .ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.Email)
            .HasColumnType(DbConstants.VarcharType)
            .HasMaxLength(DbConstants.DefaultStringMaxLength)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasColumnType(DbConstants.VarcharType)
            .IsRequired();
    }
}