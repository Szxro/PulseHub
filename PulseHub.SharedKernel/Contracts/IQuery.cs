using MediatR;

namespace PulseHub.SharedKernel.Contracts;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
