using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public static class UserErrors
{
    public static Error EmailNotUnique(string email)
        => Error.Conflit($"Email address '{email}' is already registered.");

    public static Error UsernameNotUnique(string username)
        => Error.Conflit($"Username '{username}' is already registered");

    public static Error UserNotFoundByUsernameAndEmail(string username, string email)
        => Error.NotFound($"User not found with the username '{username}' and email '{email}'");
}
