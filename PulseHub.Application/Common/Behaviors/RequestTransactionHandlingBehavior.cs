using MediatR;
using Microsoft.Extensions.Logging;
using PulseHub.Domain.Contracts;
using PulseHub.SharedKernel.Contracts;
using System.Data;

namespace PulseHub.Application.Common.Behaviors;

public class RequestTransactionHandlingBehavior<TRequest,TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : IResult
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RequestExceptionHandlingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestTransactionHandlingBehavior(
        IUnitOfWork unitOfWork,
        ILogger<RequestExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // if the transaction is not commit, when the object is disposed it's going to be automatically rollback thanks to the using statement
        using IDbTransaction transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        string commandName = typeof(TRequest).Name;

        try
        {
            TResponse response = await next();

            if (response.IsSuccess)
            {
                _logger.LogInformation(
                    "The command {commandName} was successfully completed, committing the transaction.",
                    commandName);
             
               transaction.Commit();
            }

            return response;

        } catch
        {
            _logger.LogError(
                "An unexpected error happen while trying to complete the command {commandName}, rollying back the transaction.",
                commandName);

            transaction.Rollback();

            throw;
        }
    }
}
