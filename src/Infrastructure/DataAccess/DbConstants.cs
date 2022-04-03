namespace Infrastructure.DataAccess;

public class DbConstants
{
    public const string DefaultScheme = "public";
    public const string CourseTable = "courses";
    public const string StudentTable = "students";
    public const string SystemUserTable = "users";
    public const string VarcharType = "varchar";
    public const string GuidType = "uuid";
    public const string DateOnlyType = "date";
    public const string DateTimeOffsetType = "time with time zone";
    public const string DbContextSection = "DatabaseConnectionString";
    public const string InMemoryDatabase = "TestLmsDatabase";
    public const string MigrationsAssembly = "Infrastructure";
    public const int DefaultStringMaxLength = 80;
    public const int DefaultStringLength = 25;
    public const int CommandTimeout = 60000;
}