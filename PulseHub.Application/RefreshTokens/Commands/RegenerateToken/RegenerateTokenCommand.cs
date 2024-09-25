using System.Security.Claims;
using PulseHub.Domain.Entities;
using PulseHub.Domain.Contracts;
using PulseHub.SharedKernel;
using PulseHub.SharedKernel.Contracts;
using PulseHub.Application.Common.DTOs.Responses;
using PulseHub.Domain.Errors;

namespace PulseHub.Application.RefreshTokens.Commands.RegenerateToken;

public record RegenerateTokenCommand(string token,string refreshToken): ICommand<TokenResponse>;

public class RegenerateTokenCommandHandler : ICommandHandler<RegenerateTokenCommand,TokenResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    private const string TokenUsernameType = "name";
    private const string TokenExpirateStampType = "exp";

    public RegenerateTokenCommandHandler(
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<TokenResponse>> Handle(RegenerateTokenCommand request, CancellationToken cancellationToken)
    {
        (bool isValid,ClaimsIdentity? claims) = await _tokenService.ValidateToken(request.token);

        if (!isValid)
        {
            return Result<TokenResponse>.Failure(TokenErrors.InvalidToken);
        }

        (string? username,string? expiryDateStamp) = ExtractClaims(claims);
        
        if (username is null || expiryDateStamp is null)
        {
            return Result<TokenResponse>.Failure(TokenErrors.InvalidTokenClaims);
        }

        if (!long.TryParse(expiryDateStamp, out long expiryTimestamp))
        {
            return Result<TokenResponse>.Failure(TokenErrors.InvalidTokenExpiry);
        }

        DateTime tokenExpiryDate = TimeStampToUTCDate(expiryTimestamp);

        if (DateTime.UtcNow < tokenExpiryDate)
        {
            return Result<TokenResponse>.Failure(TokenErrors.TokenStillValid);
        }

        RefreshToken? validRefreshToken = await _refreshTokenRepository.FetchValidRefreshTokenAsync(request.refreshToken, username, cancellationToken);

        if (validRefreshToken is null)
        {
            return Result<TokenResponse>.Failure(RefreshTokenErrors.InvalidRefreshToken);
        }

        validRefreshToken.IsUsed = true;

        _refreshTokenRepository.Update(validRefreshToken);

        RefreshToken newRefreshToken = new RefreshToken
        {
            Value = _tokenService.GenerateRefreshToken(),
            User = validRefreshToken.User,
        };

        _unitOfWork.ChangeTrackerToUnchanged(newRefreshToken.User);

        _refreshTokenRepository.Add(newRefreshToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TokenResponse>.Success(
            new TokenResponse
            {
                Token = _tokenService.GenerateToken(validRefreshToken.User),
                RefreshToken = newRefreshToken.Value
            });
    }

    private (string? username, string? expiryDateStamp) ExtractClaims(ClaimsIdentity? claimsIdentity)
    {
        string? expiryDateStamp = claimsIdentity?.Claims.Where(x => x.Type == TokenExpirateStampType).FirstOrDefault()?.Value;

        string? username = claimsIdentity?.Claims.Where(x => x.Type == TokenUsernameType).FirstOrDefault()?.Value;

        return (username, expiryDateStamp);
    }

    private DateTime TimeStampToUTCDate(long timeStamp)
    {
        return DateTimeOffset.FromUnixTimeSeconds(timeStamp).UtcDateTime;
    }
}
