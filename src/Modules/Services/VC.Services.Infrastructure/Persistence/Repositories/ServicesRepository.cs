using Microsoft.EntityFrameworkCore;
using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence.Repositories;

internal class ServicesRepository : BaseRepository<Service, Guid>, IServicesRepository
{
    public ServicesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => DbSet.AnyAsync(s => s.Title == title, cancellationToken);
}