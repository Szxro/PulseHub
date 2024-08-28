using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Events.User;
using PulseHub.Domain.ValueObjects;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string firstName,
    string lastName,
    string username,
    string email,
    string password) : ICommand<Result>;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IHashingService hashingService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!Email.IsValid(request.email,out Result<Email>? validEmail))
        {
            return Result.Failure(Error.ValidationFailure("Invalid email address. Please check and try again."));
        }

        if (await _userRepository.IsEmailUnique(validEmail!.Value!))
        {
            return Result.Failure(Error.ValidationFailure($"Email address '{validEmail.Value!.Value}' is already registered."));
        }

        if (await _userRepository.IsUserNameUnique(request.username))
        {
            return Result.Failure(Error.ValidationFailure($"Username '{request.username}' is already registered."));
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

        newUser.AddEvent(new SendEmailCodeEvent(newUser.Username,newUser.Email.Value));

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
