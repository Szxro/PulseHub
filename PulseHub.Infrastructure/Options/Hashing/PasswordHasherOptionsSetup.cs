using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.Hashing;

public class HashingOptionsSetup : BaseSetup<HashingOptions>
{
    public HashingOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
