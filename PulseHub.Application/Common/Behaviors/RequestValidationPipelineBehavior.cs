using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PulseHub.Application.Common.Utilities;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Extensions;
using System.Reflection;
using ValidationException = PulseHub.Application.Common.Exceptions.ValidationException;

namespace PulseHub.Application.Common.Behaviors;

public class RequestValidationPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationFailure[] failures = await ValidateAsync(request, cancellationToken);

        if (failures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse).IsGenericType
            && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type genericType = typeof(TResponse).GetGenericArguments()[0];

            MethodInfo? failureMethod = typeof(Result<>).MakeGenericType(genericType)
                                                .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
            {
                #pragma warning disable CS8600
                #pragma warning disable CS8603 
                return (TResponse)failureMethod.Invoke(null,
                                                       [CreateValidationError(failures)]);            }
        }

        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidationError(failures));
        }


        throw new ValidationException(failures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request,CancellationToken cancellationToken = default)
    {
        if (!_validators.Any())
        {
            return Array.Empty<ValidationFailure>();
        }

        ValidationContext<TRequest> validationContext = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults = await _validators.Select(validator => validator.ValidateAsync(request, cancellationToken)).WhenAll();

        ValidationFailure[] validationFailures = validationResults.Where(validdation => !validdation.IsValid)
                                                                  .SelectMany(validation => validation.Errors)
                                                                  .ToArray();
        return validationFailures;
    }

    private ValidationError CreateValidationError(ValidationFailure[] failures)
        => new ValidationError(failures.Select(ErrorHelpers.CreateErrorFromValidationFailure).ToArray());
}
