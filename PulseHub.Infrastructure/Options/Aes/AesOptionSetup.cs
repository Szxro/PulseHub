using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.Aes;

public class AesOptionSetup : BaseOptionSetup<AesOptions>
{
    public AesOptionSetup(IConfiguration configuration) : base(configuration) { }
}
