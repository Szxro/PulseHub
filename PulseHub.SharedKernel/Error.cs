using PulseHub.SharedKernel.Enums;

namespace PulseHub.SharedKernel;

public sealed class Error
{
    public string ErrorName { get; } 

    public string ErrorDescription { get; }

    public ErrorType ErrorType { get; }

    private Error(string errorName,string errorDescription,ErrorType errorType)
    {
        ErrorName = errorName;
        ErrorDescription = errorDescription;
        ErrorType = errorType;
    }

    public static Error None = new(string.Empty,string.Empty,ErrorType.None);

    public static Error Validation = new("Validation.Error","One or more validation error occurred",ErrorType.Validation);

    public static Error NotFound(string errorDescription) => new("NotFound.Error",errorDescription,ErrorType.NotFound);

    public static Error Conflit(string errorDescription) => new("Conflit.Error",errorDescription,ErrorType.Conflit);

    public static Error Exception(string errorDescription) => new("Exception.Error",errorDescription,ErrorType.Exception);
}
