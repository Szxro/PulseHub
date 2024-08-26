using FluentValidation.Results;

namespace PulseHub.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public IReadOnlyList<ValidationFailure> Failures { get; }

    public ValidationException() : base("One or more validations errors happen")
    {
        Failures = new List<ValidationFailure>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Failures = failures.ToList().AsReadOnly();
    }
}
