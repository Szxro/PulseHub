using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Enums;

namespace PulseHub.Domain.Contracts;

public interface IMediaCompressionService
{
    Result<string> ImageCompressionAndSave(Stream imageStream, long fileLength, string extension, ImageQuality quality);
}
