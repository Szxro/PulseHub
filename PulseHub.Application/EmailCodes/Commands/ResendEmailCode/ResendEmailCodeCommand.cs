using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Events.EmailCode;
using PulseHub.Domain.ValueObjects;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.EmailCodes.Commands.ResendEmailCode;

public record ResendEmailCodeCommand(string username, string email) : ICommand<Result>;

public class ResendEmailCodeCommandHandler : ICommandHandler<ResendEmailCodeCommand, Result>
{
    private readonly IEmailCodeRepository _emailCodeRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ResendEmailCodeCommandHandler(
        IEmailCodeRepository emailCodeRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _emailCodeRepository = emailCodeRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ResendEmailCodeCommand request, CancellationToken cancellationToken)
    {
        if (!Email.IsValid(request.email,out _))
        {
            return Result.Failure(Error.ValidationFailure("Invalid email address, check and try again."));
        }

        EmailCode? currentEmailCode = await _emailCodeRepository.GetNoVerifiedEmailCodeByUsernameAndEmail(request.username,request.email,cancellationToken);
        
        if (currentEmailCode is null)
        {
            return Result.Failure(Error.NotFound($"Not current unverified email code was found for the username '{request.username}' and email '{request.email}'."));
        }

        currentEmailCode.IsInvalid = true;

        _emailCodeRepository.Update(currentEmailCode);

        EmailCode newEmailCode = new EmailCode
        {
            Code = _emailService.GenerateCode(),
            User = currentEmailCode.User,
        };

        _unitOfWork.ChangeTrackerToUnchanged(newEmailCode.User);

        _emailCodeRepository.Add(newEmailCode);

        newEmailCode.AddEvent(new SendEmailCodeEvent(request.username,request.email));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
