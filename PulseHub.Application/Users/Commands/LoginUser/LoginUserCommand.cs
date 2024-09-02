using PulseHub.Application.Common.DTOs.Responses;
using PulseHub.Domain.Contracts;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Errors;
using PulseHub.Domain.Events.Users;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.Users.Commands.LoginUser;

public record LoginUserCommand(string username,string password) : ICommand<TokenResponse>;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IHashingService _hashingService;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;

    private static readonly TimeSpan LockOut = TimeSpan.FromMinutes(5);

    public LoginUserCommandHandler(
        ITokenService tokenService,
        IHashingService hashingService,
        ICacheService cacheService,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _hashingService = hashingService;
        _unitOfWork = unitOfWork;
        _refreshTokenRepository = refreshTokenRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Try to retrieve the user from the cache (it means the user is lock out)
        User? foundUser = _cacheService.Get<User>($"user-{request.username}")
            ?? await _userRepository.GetUserByUsernameAsync(request.username, cancellationToken);

        if (foundUser is null)
        {
            return Result<TokenResponse>.Failure(UserErrors.UserNotFoundByUsername(request.username));
        }

        // Check if the user is locked out
        if (foundUser.LockOutEnd > DateTime.Now)
        {
            return Result<TokenResponse>.Failure(UserErrors.UserLockOut(request.username, foundUser.LockOutEnd));
        }

        // Check if the user failed count is greater or equal to three (to lock him up)
        if (foundUser.AccessFailedCount >= 3)
        {
            await HandleLockOutAsync(foundUser,cancellationToken);

            return Result<TokenResponse>.Failure(UserErrors.UserLockOut(request.username, foundUser.LockOutEnd));
        }

        (string hash, string salt) = foundUser.Credentials.Select(x => (x.HashValue, x.SaltValue)).FirstOrDefault();

        bool isPasswordValid = _hashingService.VerifyHash(request.password, hash, salt);

        // if password is invalid increment the user access failed count
        if (!isPasswordValid)
        {
            await HandleFailedLogInAttemptAsync(foundUser,cancellationToken);

            return Result<TokenResponse>.Failure(CredentialsErrors.IncorrectCredentials(foundUser.AccessFailedCount));
        }

        RefreshToken? foundRefreshToken = await _refreshTokenRepository.GetUnusedUserRefreshTokenByUsernameAsync(request.username, cancellationToken);

        if (foundRefreshToken is not null)
        {
            foundRefreshToken.IsRevoked = true;

            _refreshTokenRepository.Update(foundRefreshToken);
        }

        // Resetting failed count after successfully log into the system
        foundUser.AccessFailedCount = 0;

        // if the user is still in the cache remove it
        _cacheService.Remove($"user-{request.username}"); 

        _userRepository.Update(foundUser);

        RefreshToken refresh = new RefreshToken
        {
            Value = _tokenService.GenerateRefreshToken(),
            User = foundUser,
        };

        _refreshTokenRepository.Add(refresh);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TokenResponse>.Success(
            new TokenResponse
            {
                Token = _tokenService.GenerateToken(foundUser),
                RefreshedToken = refresh.Value,
            });
    }

    private async Task HandleLockOutAsync(User user,CancellationToken cancellationToken = default)
    {
        user.LockOutEnd = DateTime.Now.Add(LockOut);

        user.AccessFailedCount = 0;

        // Notify user about the lock out
        user.AddEvent(new UserLockOutEvent(user.Username, user.Email.Value));

        _userRepository.Update(user);

        // Cache the user if the user is lock out
        _cacheService.Set($"user-{user.Username}", user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task HandleFailedLogInAttemptAsync(User user,CancellationToken cancellationToken = default)
    {
        user.AccessFailedCount += 1;

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
