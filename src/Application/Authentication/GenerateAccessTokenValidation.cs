namespace Application.Authentication;

public class GenerateAccessTokenValidation : AbstractValidator<GenerateAccessTokenRequest>
{
    public GenerateAccessTokenValidation()
    {
        RuleFor(x => x.Dto).NotEmpty().NotNull();
        When(x => x.Dto != null, () =>
        {
            RuleFor(x => x.Dto.Key).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Dto.Secret).NotEmpty().NotNull();
            RuleFor(x => x.Dto.Type).IsInEnum();
        });
    }
}