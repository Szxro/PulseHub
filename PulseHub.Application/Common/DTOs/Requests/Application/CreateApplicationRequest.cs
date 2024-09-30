namespace PulseHub.Application.Common.DTOs.Requests.Application;

public record CreateApplicationRequest(
    string applicationName,
    string applicationDescription,
    string providerName,
    string providerApplicationId);
