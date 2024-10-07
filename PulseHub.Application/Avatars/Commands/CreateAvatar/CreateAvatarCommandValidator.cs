using FluentValidation;

namespace PulseHub.Application.Avatars.Commands.CreateAvatar;

public class CreateAvatarCommandValidator : AbstractValidator<CreateAvatarCommand>
{
    public CreateAvatarCommandValidator()
    {
        RuleFor(x => x.file)
            .NotEmpty().WithMessage("The upload avatar can't be empty.")
            .NotNull().WithMessage("The upload avatar can't be null.");
    }
}
