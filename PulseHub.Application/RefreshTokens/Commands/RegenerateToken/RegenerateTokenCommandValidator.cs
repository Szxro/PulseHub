using FluentValidation;

namespace PulseHub.Application.RefreshTokens.Commands.RegenerateToken;

public class RegenerateTokenCommandValidator : AbstractValidator<RegenerateTokenCommand>
{
    public RegenerateTokenCommandValidator()
    {
        RuleFor(command => command.token)
            .NotNull().WithMessage("The token can't be null")
            .NotEmpty().WithMessage("The token can't be empty");

        RuleFor(command => command.refreshToken)
            .NotNull().WithMessage("The refresh token can't be null")
            .NotEmpty().WithMessage("The refresh token can't be empty")
            .MinimumLength(32).WithMessage("The refresh token length need to be 32 characters long");
    }
}
