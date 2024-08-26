using MediatR;

namespace PulseHub.SharedKernel.Contracts;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{ }
