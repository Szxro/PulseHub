using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface IApplicationRepository 
    : IRepositoryWriter<Application>
{
    Task<bool> IsApplicationNameNotUnique(string name);
}
