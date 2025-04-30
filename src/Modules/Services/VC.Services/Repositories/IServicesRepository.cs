namespace VC.Services.Repositories;

public interface IServicesRepository
{
    Task<ICollection<Service>> GetByTenantAsync(Guid id, CancellationToken cancellationToken);
    Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ICollection<Service>> GetByFilterAsync(string filter, decimal price, CancellationToken cancellationToken);
    Task AddAsync(Service service, CancellationToken cancellationToken);
    Task UpdateAsync(Service service, CancellationToken cancellationToken);
    Task RemoveAsync(Service service, CancellationToken cancellationToken);
}