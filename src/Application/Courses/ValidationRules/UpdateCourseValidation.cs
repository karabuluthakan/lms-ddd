namespace Application.Courses.ValidationRules;

public class UpdateCourseValidation : AbstractValidator<UpdateCourseCommandRequest>
{
    private readonly ICourseRepository _repository;
    private readonly CancellationToken _cancellationToken;

    public UpdateCourseValidation(ICourseRepository repository)
    {
        _repository = Guard.Against.Null(repository);
        _cancellationToken = CancellationToken.None;

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEmpty()
            .Must(x => Guid.TryParse(x.ToString(), out _));

        RuleFor(x => x.Dto).NotEmpty().NotNull();

        When(x => x.Dto != null, () =>
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(80)
                .Must((data, value) => CheckName(data.Id, value).Equals(true));

            RuleFor(x => x.Dto.CoursePrefix)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10)
                .Must((data, value) => CheckCoursePrefixAsync(data.Id, value).Equals(true));
        });
    }

    private bool CheckName(Guid id, string value)
    {
        var data = _repository.GetAsync(
            x => !x.Id.Equals(id) && x.Name.ToLowerInvariant().Equals(value.ToLowerInvariant()),
            _cancellationToken).Result;
        return data.SingleOrDefault() is null;
    }

    private bool CheckCoursePrefixAsync(Guid id, string value)
    {
        var data = _repository.GetAsync(
            x => x.CoursePrefix.ToUpperInvariant().Equals(value.ToUpperInvariant()),
            _cancellationToken).Result;
        return data.SingleOrDefault() is null;
    }
}