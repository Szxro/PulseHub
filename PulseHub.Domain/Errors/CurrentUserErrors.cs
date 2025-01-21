using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public class CurrentUserErrors
{
    public static Error CurrentUserNotFound
        => Error.NotFound("The current user was not found!!");
}
