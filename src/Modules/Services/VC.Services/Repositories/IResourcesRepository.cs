namespace VC.Services.Repositories;

public interface IResourcesRepository
{
    Task<ICollection<Resource>> GetByTenantAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<Resource>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task AddAsync(Resource service, CancellationToken cancellationToken = default);
    Task UpdateAsync(Resource service, CancellationToken cancellationToken = default);
    Task RemoveAsync(Resource service, CancellationToken cancellationToken = default);
}