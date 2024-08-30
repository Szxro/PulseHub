using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Errors;
using PulseHub.Domain.Events.EmailCode;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.EmailCodes.Commands.VerifyEmailCode;

public record VerifyEmailCodeCommand (string code): ICommand;

public class VerifyEmailCodeCommandHandler : ICommandHandler<VerifyEmailCodeCommand>
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
            return Result.Failure(EmailCodeErrors.EmailCodeNotFound(request.code));
        }

        Result result = ValidateEmailCode(emailCode);

        if (result.IsFailure) return result; 

        emailCode.IsVerified = true;

        _emailCodeRepository.Update(emailCode);

        emailCode.AddEvent(new SendWelcomeEmailEvent(emailCode.User.Username,emailCode.User.Email.Value));

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    private static Result ValidateEmailCode(EmailCode emailCode)
        => emailCode switch
        {
            { IsVerified:true } => Result.Failure(EmailCodeErrors.EmailCodeAlreadyVerified(emailCode.Code)),
            { IsInvalid: true } => Result.Failure(EmailCodeErrors.EmailCodeAlreadyInvalid(emailCode.Code)),
            { IsExpired: true } => Result.Failure(EmailCodeErrors.EmailCodeAlreadyExpired(emailCode.Code)),
            _ => Result.Success(),
        };
}
