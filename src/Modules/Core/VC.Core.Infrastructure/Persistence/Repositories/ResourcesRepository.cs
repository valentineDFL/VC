using VC.Core.Repositories;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence.Repositories;

internal class ResourcesRepository : BaseRepository<Resource, Guid>, IResourcesRepository
{
    public ResourcesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}