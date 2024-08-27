using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.Hashing;

public class HashingOptions : IOptionSetup
{
    public string SectionName => "HashingOptions";

    public int SaltSize { get; set; }

    public int HashSize { get; set; }

    public int Iterations { get; set; }
}
