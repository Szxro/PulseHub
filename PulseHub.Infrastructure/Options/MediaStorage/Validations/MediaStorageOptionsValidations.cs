using FluentValidation;

namespace PulseHub.Infrastructure.Options.MediaStorage.Validations;

public class MediaStorageOptionsValidations : AbstractValidator<MediaStorageOptions>
{
    public MediaStorageOptionsValidations()
    {
        RuleFor(x => x.Path)
            .NotEmpty().WithMessage("The media storage path can't be empty")
            .NotNull().WithMessage("The media storage path can't be null");

        RuleFor(x => x.Path)
            .Must(Path.IsPathFullyQualified)
            .When(x => !string.IsNullOrEmpty(x.Path))
            .WithMessage("The media storage path is not a absolute path"); 
        // A fully qualified path means that the path includes the drive letter (on Windows) or the root directory (on Unix-based systems)
    }
}
