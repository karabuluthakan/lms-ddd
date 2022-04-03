namespace Application.Courses.ValidationRules;

public class CreateCourseValidation : AbstractValidator<CreateCourseCommandRequest>
{
    private readonly ICourseRepository _repository;

    public CreateCourseValidation(ICourseRepository repository)
    {
        _repository = Guard.Against.Null(repository);

        RuleFor(x => x.Dto).NotNull().NotEmpty();
        When(x => x.Dto != null, () =>
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(80)
                .MustAsync(async (value, cancellationToken) =>
                    (await CheckNameAsync(value, cancellationToken)).Equals(true));

            RuleFor(x => x.Dto.CoursePrefix)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10)
                .MustAsync(async (value, cancellationToken) =>
                    (await CheckCoursePrefixAsync(value, cancellationToken)).Equals(true));
        });
    }

    private async ValueTask<bool> CheckNameAsync(string value, CancellationToken cancellationToken)
    {
        var data = await _repository
            .GetAsync(x => x.Name.ToLowerInvariant().Equals(value.ToLowerInvariant()), cancellationToken);
        return data.SingleOrDefault() is null;
    }

    private async ValueTask<bool> CheckCoursePrefixAsync(string value, CancellationToken cancellationToken)
    {
        var data = await _repository
            .GetAsync(x => x.CoursePrefix.ToUpperInvariant().Equals(value.ToUpperInvariant()), cancellationToken);
        return data.SingleOrDefault() is null;
    }
}