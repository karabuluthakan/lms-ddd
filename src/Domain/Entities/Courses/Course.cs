#pragma warning disable CS8618
namespace Domain.Entities.Courses;

public class Course : Entity
{
    public string Name { get; }
    public string CoursePrefix { get; }
    public List<Student> Students { get; private set; }

    public Course(string coursePrefix, string name)
    {
        CheckRule(new CoursePrefixRule(coursePrefix));
        CheckRule(new CourseNameRule(name));
        CoursePrefix = coursePrefix.Trim().ToUpperInvariant();
        Name = name.Trim();
        Students ??= new List<Student>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coursePrefix"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Course Create(string coursePrefix, string name)
    {
        return new Course(coursePrefix, name);
    }

    public Course AddStudent(Student item)
    {
        if (item is null)
        {
            return this;
        }

        Students ??= new List<Student>();
        // A student can only enroll in the same course once.
        if (Students.Any(x => x.Id.Equals(item.Id)))
        {
            return this;
        }

        Students.Add(item);
        return this;
    }

    public Course AddStudents(List<Student> items)
    {
        if (items is null || !items.Any())
        {
            return this;
        }

        Students ??= new List<Student>();
        // A student can only enroll in the same course once.
        var ids = items.Select(x => x.Id);
        var exist = Students.Where(x => ids.Contains(x.Id))?.ToList();

        if (exist is not null && exist.Any())
        {
            var existIds = exist.Select(x => x.Id);
            items.RemoveAll(x => existIds.Contains(x.Id));
        }

        if (items.Any())
        {
            Students.AddRange(items);
        }

        return this;
    }

    /// <summary>
    ///  For orm constructor
    /// </summary>
    public Course()
    {
        Students ??= new List<Student>();
    }
}