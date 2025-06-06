using Microsoft.EntityFrameworkCore;
using VC.Core.Repositories;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence.Repositories;

internal class ServicesRepository : BaseRepository<Service, Guid>, IServicesRepository
{
    public ServicesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExistsAsync(string title, CancellationToken cancellationToken = default)
        => await DbSet.AnyAsync(s => s.Title == title, cancellationToken);

    public async Task<Service?> GetByAssignedEmployeeIdAsync(Guid employeeId)
        => await DbSet.FirstOrDefaultAsync(s => s.EmployeeAssignments.Any(e => e.EmployeeId == employeeId));
}