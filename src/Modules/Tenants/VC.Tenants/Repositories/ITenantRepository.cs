using VC.Tenants.Entities;
using VC.Utilities;

namespace VC.Tenants.Repositories;

public interface ITenantRepository
{
    public Task<Tenant?> GetAsync();

    public Task AddAsync(Tenant tenant);

    public void Remove(Tenant tenant);

    public Task UpdateAsync(Tenant tenant);
}