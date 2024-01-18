using FluentValidation;

namespace RabbitMQDemo.Application.PipelineBehaviors.Validation;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var validationResult = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationResult(validationFailure.PropertyName, validationFailure.ErrorMessage))
            .ToList();

        if (validationResult.Any())
        {
            throw new ValidationException(validationResult);
        }

        var response = await next();

        return response;
    }
}
