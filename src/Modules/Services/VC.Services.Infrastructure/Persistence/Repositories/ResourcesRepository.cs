using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence.Repositories;

internal class ResourcesRepository : IResourcesRepository
{
    public Task<ICollection<Resource>> GetByTenantAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Resource service, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Resource service, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Resource service, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}