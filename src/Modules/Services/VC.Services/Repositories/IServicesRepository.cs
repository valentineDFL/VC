using VC.Services.Entities;

namespace VC.Services.Repositories;

public interface IServicesRepository : IRepositoryBase<Service>
{
    public Task<ICollection<Service>> GetByTenantAsync(Guid id, CancellationToken cancellationToken);
    public Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ICollection<Service>> GetByFilterAsync(string filter, decimal price, CancellationToken cancellationToken);
}
