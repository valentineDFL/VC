using VC.Tenants.Entities;

namespace VC.Tenants.Repositories;

public interface ITenantRepository : IRepositoryBase<Tenant>
{
    public Task<Tenant?> GetAsync();
}