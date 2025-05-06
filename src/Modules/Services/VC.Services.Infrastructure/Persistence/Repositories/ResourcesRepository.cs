using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence.Repositories;

internal class ResourcesRepository : BaseRepository<Resource, Guid>, IResourcesRepository
{
    public ResourcesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}