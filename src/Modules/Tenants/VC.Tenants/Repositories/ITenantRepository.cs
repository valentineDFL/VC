using VC.Tenants.Entities;
using VC.Utilities;

namespace VC.Tenants.Repositories;

public interface ITenantRepository : IRepositoryBase<Tenant>
{
    public Task<Tenant?> GetAsync();
}