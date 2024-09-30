using PulseHub.SharedKernel;

namespace PulseHub.Domain.Errors;

public class ProviderErrors
{
    public static Error ProviderNotFoundByName(string providerName)
        => Error.NotFound($"The provider with the name {providerName} was not found");
}
