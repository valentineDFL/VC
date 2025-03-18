using VC.Tenants.Infrastructure.Persistence;

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
