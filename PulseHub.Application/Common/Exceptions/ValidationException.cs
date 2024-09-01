using FluentValidation.Results;
using PulseHub.Application.Common.Utilities;
using PulseHub.SharedKernel;

namespace PulseHub.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public Error[] Failures { get; }

    public ValidationException() : base("One or more validations errors happen")
    {
        Failures = Array.Empty<Error>();
    }

    public ValidationException(ValidationFailure[] failures) : this()
    {
        Failures = CreateValidationError(failures);
    }

    private Error[] CreateValidationError(IEnumerable<ValidationFailure> failures)
        => failures.Select(ErrorHelpers.CreateErrorFromValidationFailure).ToArray();
}
