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

    public static Error UserNotFoundByUsername(string username)
        => Error.NotFound($"User not found with the username '{username}'.");

    public static Error UserLockOut(string username, DateTime dateTime)
    {
        TimeSpan timeRemaining = dateTime - DateTime.Now;

        return Error.Validation(string.Empty,"UserLockOut.Error", $"The User with the username '{username}' is lock out, lock out end in {FormatTimeRemaining(timeRemaining)}");
    }

    private static string FormatTimeRemaining(TimeSpan timeSpan)
    {
        return timeSpan switch
        {
            { TotalDays: > 1} => $"{Math.Floor(timeSpan.TotalDays)} days",
            { TotalHours: > 1 } => $"{Math.Floor(timeSpan.TotalHours)} hours",
            { TotalMinutes: > 1 } => $"{Math.Floor(timeSpan.TotalMinutes)} minutes",
            _ => $"{(int)timeSpan.TotalSeconds} seconds"
        };
    }
}
