namespace PulseHub.Application.Common.DTOs.Responses;

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
