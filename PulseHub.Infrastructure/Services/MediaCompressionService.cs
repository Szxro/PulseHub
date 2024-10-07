using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PulseHub.SharedKernel.Results;
using PulseHub.Domain.Contracts;
using PulseHub.Infrastructure.Options.MediaStorage;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Enums;
using Encoder = System.Drawing.Imaging.Encoder;

namespace PulseHub.Infrastructure.Services;

public class MediaCompressionService : IMediaCompressionService
{
    private static readonly long MaxImageSize = 2 * 1024 * 1024; // 2MB in bytes (2 * 1,048,576 = 2,097,152);

    private static readonly string[] AcceptedImageExtensions = [".jpeg", ".png", ".jpg"];

    private readonly MediaStorageOptions _options;
    private readonly ILogger<MediaCompressionService> _logger;

    public MediaCompressionService(
        IOptions<MediaStorageOptions> options,
        ILogger<MediaCompressionService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    #pragma warning disable CA1416 // Validate platform compatibility (Windows 6.1+)
    public Result<CompressionResult> ImageCompressionAndSave(Stream imageStream,long fileLength,string extension,ImageQuality quality)
    {
        if (fileLength > MaxImageSize || !AcceptedImageExtensions.Contains(extension))
        {
            return Result<CompressionResult>.Failure(Error.Validation(string.Empty, "Invalid file", "The file size or extension is not allowed."));
        }

        // If the directory dont exists, create it. 
        if (!Directory.Exists(_options.Path))
        {
            Directory.CreateDirectory(_options.Path);
        }

        // Do no trust in the filename of the current upload file (create a random one)
        string filename = Guid.NewGuid().ToString();

        string destinationPath = Path.Combine(_options.Path, $"{filename}_compressed{extension}");   

        try
        {
            // Creating an image object from the current stream
            using (Image imageSource = Image.FromStream(imageStream))
            {
                // Creating a file and adding the output of the file stream to the file
                using (FileStream fileStream = new FileStream(destinationPath, FileMode.Create))
                {
                    // Getting an image encoder base on the extension of the media
                    ImageCodecInfo? imageEncoder = GetImageEncoder(GetImageFormat(extension));

                    if (imageEncoder is null)
                    {
                        _logger.LogError("Could not find an appropriate encoder for the {extension} format",extension);

                        return Result<CompressionResult>.Failure(Error.Validation(string.Empty, "Unsupported format", "The image format is not supported."));
                    }

                    // Creates an encoder parameter base on the image quality enum provide (return: GUID)
                    using EncoderParameter encoderParameter = GetImageEncoderParameter(quality);

                    // Create an encoder parameters with one paramater object
                    using EncoderParameters encodersParameters = new EncoderParameters(1)
                    {
                        Param = { [0] = encoderParameter }
                    };

                    // Saving the image to the specify stream
                    imageSource.Save(fileStream, imageEncoder, encodersParameters);
                    // Saving the compress image by buffering strategy (load the complete file into memory, great for small files)

                    return Result<CompressionResult>.Success(new CompressionResult(filename,
                                                                                   destinationPath,
                                                                                   fileLength,
                                                                                   imageSource.Size.Height,
                                                                                   imageSource.Size.Width,
                                                                                   imageSource.RawFormat.ToString()));
                }
            }
        } catch (Exception ex)
        {
            _logger.LogError(
                "An unexpected error occurred while compressing the image. File path: {destinationPath}, Error: {errorMessage}",
                destinationPath,
                ex.Message);

            throw;
        }
    }

    private ImageCodecInfo? GetImageEncoder(ImageFormat format)
    {
        // Array of built in GDI+ image encoders (Avaliables encoders)
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        // An encoder translates the data in an Image or Bitmap object into a designated disk file format.
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }

        return null;
    }

    private ImageFormat GetImageFormat(string extension) => 
        extension.ToLower() switch
    {
        ".jpg" or ".jpeg" => ImageFormat.Jpeg,
        ".png" => ImageFormat.Png,
        _ => throw new NotSupportedException()
    };

    private EncoderParameter GetImageEncoderParameter(ImageQuality imageQuality) =>
        // The size of the image is going to be base on the quality provide
        imageQuality switch
        {
            ImageQuality.Lowest => new EncoderParameter(Encoder.Quality, 0L), // Max compression (Bad quality) 
            ImageQuality.Good => new EncoderParameter(Encoder.Quality, 50L), // Good compression (Good quality) 
            ImageQuality.Balacing => new EncoderParameter(Encoder.Quality, 80L), // Medium compression (Greater quality)
            ImageQuality.Highest => new EncoderParameter(Encoder.Quality, 100L), // No compression at all (Highest posible quality)
            _ => throw new Exception("The quality provide is invalid.")
        };
}
