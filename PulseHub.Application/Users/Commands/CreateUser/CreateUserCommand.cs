using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Errors;
using PulseHub.Domain.Events.EmailCode;
using PulseHub.Domain.ValueObjects;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string firstName,
    string lastName,
    string username,
    string email,
    string password) : ICommand;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashingService _hashingService;
    private readonly IEmailService _emailService;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IHashingService hashingService,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _hashingService = hashingService;
        _emailService = emailService;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!Email.IsValid(request.email,out Result<Email>? validEmail))
        {
            return Result.Failure(Email.InvalidEmail);
        }

        if (await _userRepository.IsEmailUnique(validEmail!.Value!))
        {
            return Result.Failure(UserErrors.EmailNotUnique(request.email));
        }

        if (await _userRepository.IsUserNameUnique(request.username))
        {
            return Result.Failure(UserErrors.UsernameNotUnique(request.username));
        }

        string hash = _hashingService.GetHash(request.password, out byte[] salt);

        User newUser = new User
        {
            FirstName = request.firstName,
            Lastname = request.lastName,
            Email = validEmail.Value!,
            Username = request.username
        };

        newUser.AddCredentials(
            new Credentials
            {
                HashValue = hash,
                SaltValue = Convert.ToHexString(salt),
            });

        newUser.AddEmailCode(
            new EmailCode
            {
                Code = _emailService.GenerateCode()
            });

        newUser.AddEvent(new SendEmailCodeEvent(newUser.Username,newUser.Email.Value));

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
