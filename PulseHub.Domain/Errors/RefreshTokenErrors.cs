using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public static class RefreshTokenErrors
{
    public static Error InvalidRefreshToken
        => Error.Validation(string.Empty, "InvalidRefreshToken.Error", "The provided refresh token is invalid or expired.");
}
