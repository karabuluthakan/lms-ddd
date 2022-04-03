namespace Application.PipelineBehaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validators"></param>
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators.Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null).ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return next();
    }
}