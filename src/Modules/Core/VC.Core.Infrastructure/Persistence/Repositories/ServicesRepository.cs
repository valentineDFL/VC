using Microsoft.EntityFrameworkCore;
using VC.Core.Repositories;

namespace VC.Core.Infrastructure.Persistence.Repositories;

internal class ServicesRepository : BaseRepository<Service, Guid>, IServicesRepository
{
    public ServicesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => DbSet.AnyAsync(s => s.Title == title, cancellationToken);
}