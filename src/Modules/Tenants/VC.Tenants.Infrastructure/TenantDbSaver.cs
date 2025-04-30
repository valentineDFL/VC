using VC.Tenants.Infrastructure.Persistence;
using VC.Tenants.UnitOfWork;

namespace VC.Tenants.Infrastructure;

public class TenantDbSaver : IDbSaver
{
    private readonly TenantsDbContext _context;

    public TenantDbSaver(TenantsDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}