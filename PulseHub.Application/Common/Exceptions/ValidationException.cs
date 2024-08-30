using FluentValidation.Results;
using PulseHub.SharedKernel;

namespace PulseHub.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public Error[] Failures { get; }

    public ValidationException() : base("One or more validations errors happen")
    {
        Failures = Array.Empty<Error>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Failures = CreateValidationError(failures);
    }

    private Error[] CreateValidationError(IEnumerable<ValidationFailure> failures)
    => failures.Select(failure => Error.Validation(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage)).ToArray();
}
