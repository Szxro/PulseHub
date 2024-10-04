using Microsoft.Extensions.Configuration;
using PulseHub.Infrastructure.Options.Base;

namespace PulseHub.Infrastructure.Options.MediaStorage;

public class MediaStorageOptionsSetup : BaseOptionSetup<MediaStorageOptions>
{
    public MediaStorageOptionsSetup(IConfiguration configuration) : base(configuration) { }
}
