namespace PulseHub.SharedKernel;
using PulseHub.SharedKernel.Enums;

public class ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(string.Empty,
               "Validation.Error",
               "One or more validation errors occurred.",
               ErrorType.Validation)
    {
        Errors = errors;
    }

    public Error[] Errors { get; set; }
}
