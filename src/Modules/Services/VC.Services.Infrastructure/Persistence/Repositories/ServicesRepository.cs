using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence.Repositories;

internal class ServicesRepository : IServicesRepository
{
    public Task<ICollection<Service>> GetByTenantAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Service>> GetByFilterAsync(string filter, decimal price, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Service service, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Service service, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Service service, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string title)
    {
        throw new NotImplementedException();
    }
}