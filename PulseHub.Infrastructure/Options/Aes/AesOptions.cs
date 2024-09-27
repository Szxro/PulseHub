using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.Aes;

public class AesOptions : IOptionSetup
{
    public string SectionName => "AesOptions";

    public string PrivateKey { get; set; } = string.Empty;
}
