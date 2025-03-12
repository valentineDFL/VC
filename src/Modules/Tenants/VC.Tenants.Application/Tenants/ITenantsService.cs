using FluentResults;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants;

public interface ITenantsService
{
    public Task<Result> CreateAsync(CreateTenantParams @params);

    public Task<Result> DeleteAsync(Guid tenantId);

    public Task<Result<Tenant>> GetByIdAsync(Guid tenantId);

    public Task<Result> UpdateAsync(UpdateTenantParams @params);
}
