#pragma warning disable CS8618
namespace Domain.Entities.Students;

public class Student : Entity
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public DateOnly BirthDate { get; }

    public List<Course> Courses { get; private set; }

    public Student(string firstName, string lastName, string email, string plainPassword, DateTime birthDate)
    {
        CheckRule(new StudentNameRule(firstName, nameof(FirstName)));
        CheckRule(new StudentNameRule(lastName, nameof(LastName)));
        CheckRule(new StudentDateOfBirthRule(birthDate));
        CheckRule(new StudentEmailRule(email));
        CheckRule(new StudentPasswordRule(plainPassword));
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim().ToLowerInvariant();
        PasswordHash = CryptoHandler.GeneratePassword(plainPassword);
        BirthDate = DateOnly.FromDateTime(birthDate);
    }

    public Student AddCourse(Course item)
    {
        if (item is null)
        {
            return this;
        }

        Courses ??= new List<Course>();
        // Student can only enroll in the course once.
        if (Courses.Any(x => x.Id.Equals(item.Id)))
        {
            return this;
        }

        Courses.Add(item);
        return this;
    }

    public Student AddCourses(List<Course> items)
    {
        if (items is null || !items.Any())
        {
            //Todo: Business rule add
            return this;
        }

        Courses ??= new List<Course>();
        // Student can only enroll in the course once.
        var ids = items.Select(x => x.Id);
        var exist = Courses.Where(x => ids.Contains(x.Id))?.ToList();

        if (exist is not null && exist.Any())
        {
            var existIds = exist.Select(x => x.Id);
            items.RemoveAll(x => existIds.Contains(x.Id));
        }

        if (items.Any())
        {
            Courses.AddRange(items);
        }

        return this;
    }

    public Student()
    {
    }
}