using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Events.EmailCode;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.EmailCodes.Commands.VerifyEmailCode;

public record VerifyEmailCodeCommand (string code): ICommand<Result>;

public class VerifyEmailCodeCommandHandler : ICommandHandler<VerifyEmailCodeCommand, Result>
{
    private readonly IEmailCodeRepository _emailCodeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VerifyEmailCodeCommandHandler(
        IEmailCodeRepository emailCodeRepository,
        IUnitOfWork unitOfWork)
    {
        _emailCodeRepository = emailCodeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(VerifyEmailCodeCommand request, CancellationToken cancellationToken)
    {
        EmailCode? emailCode = await _emailCodeRepository.GetEmailCodeByCode(request.code, cancellationToken);

        if (emailCode is null)
        {
            return Result.Failure(Error.NotFound($"The email code '{request.code}' was not found."));
        }

        if (emailCode.IsVerified)
        {
            return Result.Failure(Error.ValidationFailure($"The email code '{request.code}' was already validated."));
        }

        if (emailCode.IsInvalid)
        {
            return Result.Failure(Error.ValidationFailure($"The email code '{request.code}' is invalid."));
        }

        emailCode.IsVerified = true;

        _emailCodeRepository.Update(emailCode);

        emailCode.AddEvent(new SendWelcomeEmailEvent(emailCode.User.Username,emailCode.User.Email.Value));

        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}
