namespace PulseHub.Application.Common.DTOs.Responses;

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;

    public string RefreshedToken { get; set; } = string.Empty;
}
