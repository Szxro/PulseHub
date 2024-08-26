using MediatR;
using Microsoft.Extensions.Logging;

namespace PulseHub.Application.Common.Behaviors;

public class RequestExceptionHandlingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<RequestPerformancePipelineBehavior<TRequest, TResponse>> _logger;

    public RequestExceptionHandlingPipelineBehavior(ILogger<RequestPerformancePipelineBehavior<TRequest,TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            TResponse response = await next();

            return response;
        
        } catch (Exception ex)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogInformation(
                "An unhandled error occurred while trying to completed the request {requestName} with the error message: {message}",
                requestName,
                ex.Message);

            throw;
        }
    }
}
