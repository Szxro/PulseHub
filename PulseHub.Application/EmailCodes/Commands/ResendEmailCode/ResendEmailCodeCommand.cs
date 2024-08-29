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
    private readonly IUserRepository _userRepository;
    private readonly IEmailCodeRepository _emailCodeRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ResendEmailCodeCommandHandler(
        IUserRepository userRepository,
        IEmailCodeRepository emailCodeRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
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

        User? currentUser = await _userRepository.GetUserByUsernameAndEmailAsync(request.username,request.email,cancellationToken);

        if (currentUser is null)
        {
            return Result.Failure(Error.NotFound($"User not found with the username '{request.username}' and email '{request.email}'"));
        }

        if (await _emailCodeRepository.IsUserEmailCodeVerifiedByUsernameAndEmailAsync(request.username,request.email,cancellationToken))
        {
            return Result.Failure(Error.ValidationFailure($"The current user with the username '{request.username}' and email '{request.email}' is already verified"));
        }

        EmailCode? currentEmailCode = await _emailCodeRepository.GetCurrentActiveEmailCodeByUsernameAndEmailAsync(request.username,request.email,cancellationToken);

        if (currentEmailCode is not null)
        {
            currentEmailCode.IsInvalid = true;

            _emailCodeRepository.Update(currentEmailCode);
        }

        EmailCode newEmailCode = new EmailCode
        {
            Code = _emailService.GenerateCode(),
            User = currentUser,
        };

        _unitOfWork.ChangeTrackerToUnchanged(newEmailCode.User);

        _emailCodeRepository.Add(newEmailCode);

        newEmailCode.AddEvent(new SendEmailCodeEvent(request.username, request.email));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
