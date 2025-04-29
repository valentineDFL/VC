namespace VC.Services.Repositories;

public interface IServicesRepository
{
    Task<ICollection<Service>> GetByTenantAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<Service>> GetByFilterAsync(string filter, decimal price, CancellationToken cancellationToken = default);
    Task AddAsync(Service service, CancellationToken cancellationToken = default);
    Task UpdateAsync(Service service, CancellationToken cancellationToken = default);
    Task RemoveAsync(Service service, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string title);
}