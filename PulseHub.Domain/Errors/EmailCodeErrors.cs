using PulseHub.Domain.Entities;
using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public static class EmailCodeErrors
{
    public static Error EmailCodeNotFound(string code)
        => Error.NotFound($"The email code '{code}' was not found.");

    public static Error EmailCodeAlreadyVerified(string code)
        => Error.Validation(string.Empty, "EmailCodeVerified.Error", $"The email code '{code}' is already verify.");

    public static Error EmailCodeAlreadyInvalid(string code)
        => Error.Validation(string.Empty, "EmailCodeInvalid.Error", $"The email code '{code}' is already invalid.");

    public static Error EmailCodeAlreadyExpired(string code)
        => Error.Validation(string.Empty, "EmailCodeExpired.Error", $"The email code '{code}' is already expired.");

    public static Error UserAlreadyVerified(string username, string email)
        => Error.Validation(string.Empty, "UserVerified.Error", $"The current user with the username '{username}' and email '{email}' is already verified");
}
