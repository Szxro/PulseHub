using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public static class TokenErrors
{
    public static Error InvalidToken
        => Error.Validation(string.Empty, "InvalidToken.Error", "The provided token is invalid.");

    public static Error InvalidTokenClaims 
        => Error.Validation(string.Empty, "InvalidTokenClaims.Error", "Token does not contain a valid username or expire time stamp.");

    public static Error InvalidTokenExpiry
        => Error.Validation(string.Empty, "InvalidTokenExpiry.Error", "Token expiration timestamp is invalid.");

    public static Error TokenStillValid
        => Error.Validation(string.Empty, "TokenStillValid.Error", "The provided token is still valid.");
}
