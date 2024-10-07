using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Results;
using PulseHub.SharedKernel.Enums;

namespace PulseHub.Domain.Contracts;

public interface IMediaCompressionService
{
    Result<CompressionResult> ImageCompressionAndSave(Stream imageStream, long fileLength, string extension, ImageQuality quality);
}
