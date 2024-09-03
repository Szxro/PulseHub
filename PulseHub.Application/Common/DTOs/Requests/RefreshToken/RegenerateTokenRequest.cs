namespace PulseHub.Application.Common.DTOs.Requests.RefreshToken;

public record RegenerateTokenRequest(string token,string refreshToken);
