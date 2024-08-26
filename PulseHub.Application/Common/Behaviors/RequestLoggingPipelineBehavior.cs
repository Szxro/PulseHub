using MediatR;
using Microsoft.Extensions.Logging;
using PulseHub.SharedKernel.Contracts;

namespace PulseHub.Application.Common.Behaviors;

public class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : IResult
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation(
            "Processing the request {request} at {date} with {time}",
            requestName,
            DateTime.Now.ToShortDateString(),
            DateTime.Now.ToShortTimeString());

        TResponse response = await next();

        if (response.IsSuccess)
        {
            _logger.LogInformation("Completed the request {request} at {date} with {time}",
                requestName,
                DateTime.Now.ToShortDateString(),
                DateTime.Now.ToShortTimeString());

            return response;
        }

        _logger.LogWarning(
            "Completed the request {request} with an error {errorName} at {date} with {time}",
            requestName,
            response.Error.ErrorName,
            DateTime.Now.ToShortDateString(),
            DateTime.Now.ToShortTimeString());

        return response;
    }
}
