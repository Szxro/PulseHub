using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public static class CredentialsErrors
{
    public static Error IncorrectCredentials(int attempts)
        => Error.Validation(string.Empty,"IncorrectCredentials.Error", $"The provided credentials are incorrect,retry count {attempts}/3");

    public static Error InvalidCredentials
        => Error.Validation(string.Empty, "InvalidCredentials.Error", "The provided credentials are invalid, contact the tecnical support to fix the issue.");
}
