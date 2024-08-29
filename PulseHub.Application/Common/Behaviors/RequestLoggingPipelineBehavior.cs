using MediatR;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.Common.Behaviors;

public class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResult
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestLoggingPipelineBehavior(
        ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        string username = _currentUserService.GetCurrentUser() ?? "System";

        _logger.LogInformation("Processing the request {request} requested by the user {user}",
            requestName,
            username);

        TResponse response = await next();

        if (response.IsSuccess)
        {
            _logger.LogInformation("Completed the request {request} requested by the user {user}",
                requestName,
                username);

            return response;
        }

        _logger.LogWarning("Completed the request {request} requested by the user {user} with an error {errorName}",
            requestName,
            username,
            response.Error.ErrorName);

        return response;
    }
}