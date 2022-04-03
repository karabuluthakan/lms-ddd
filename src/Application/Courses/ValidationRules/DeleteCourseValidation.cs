namespace Application.Courses.ValidationRules;

public class DeleteCourseValidation : AbstractValidator<DeleteCourseCommandRequest>
{
    public DeleteCourseValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEmpty()
            .Must(x => Guid.TryParse(x.ToString(), out _));
    }
}