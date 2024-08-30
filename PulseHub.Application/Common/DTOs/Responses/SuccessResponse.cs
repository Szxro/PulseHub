namespace PulseHub.Application.Common.DTOs.Responses;

public class SuccessResponse
{
    public string Detail { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public int Status { get; set; }

    public object? Data { get; set; }
}
