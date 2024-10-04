using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Infrastructure.Options.MediaStorage;

public class MediaStorageOptions : IOptionSetup
{
    public string SectionName => "MediaStorage";

    public string Path { get; set; } = string.Empty;
}
