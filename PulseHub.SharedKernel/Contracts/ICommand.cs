using MediatR;

namespace PulseHub.SharedKernel.Contracts;

public interface ICommand<out TResponse> : IRequest<TResponse> { }

public interface ICommand : IRequest { }
