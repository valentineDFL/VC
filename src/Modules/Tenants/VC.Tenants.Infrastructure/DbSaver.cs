using VC.Tenants.Infrastructure.Persistence;
using VC.Tenants.UnitOfWork;

namespace VC.Tenants.Infrastructure;

public class DbSaver : IDbSaver
{
    private readonly TenantsDbContext _context;

    public DbSaver(TenantsDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
