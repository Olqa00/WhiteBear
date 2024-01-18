namespace WhiteBear.Application.Common.Behaviors;

using FluentValidation;
using ValidationException = WhiteBear.Application.Common.Exceptions.ValidationException;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any() is false)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(validatorResult => validatorResult.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(result => result.Errors.Count is > 0)
            .SelectMany(result => result.Errors)
            .Select(error => error.ErrorMessage)
            .ToList();

        if (failures.Count is > 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
