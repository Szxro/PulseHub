using PulseHub.SharedKernel.Enums;

namespace PulseHub.SharedKernel;

public class Error
{
    public string PropertyName { get; set; }

    public string ErrorCode { get; }

    public string Description { get; }

    public ErrorType Type { get; }

    protected Error(
        string propertyName,
        string errorCode,
        string errorDescription,
        ErrorType errorType)
    {
        PropertyName = propertyName;
        ErrorCode = errorCode;
        Description = errorDescription;
        Type = errorType;
    }

    public static Error None = new(string.Empty,string.Empty,string.Empty,ErrorType.None);

    public static Error NotFound(string errorDescription) => new(string.Empty,"NotFound.Error",errorDescription,ErrorType.NotFound);

    public static Error Conflit(string errorDescription) => new(string.Empty,"Conflit.Error",errorDescription,ErrorType.Conflict);

    public static Error Validation(string propertyName,string errorCode,string errorDescription) => new(propertyName, errorCode, errorDescription, ErrorType.Validation); 
}
