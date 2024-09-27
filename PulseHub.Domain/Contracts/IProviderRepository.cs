using PulseHub.Domain.Entities;

namespace PulseHub.Domain.Contracts;

public interface IProviderRepository : IRepositoryWriter<Provider>
{
    Task<Provider?> GetProviderByNameAsync(string providerName);
}
