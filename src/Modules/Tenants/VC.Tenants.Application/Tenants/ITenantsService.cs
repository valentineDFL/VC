using FluentResults;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Tenants;

public interface ITenantsService
{
    public Task<Result> CreateAsync(CreateTenantParams @params);

    public Task<Result> DeleteAsync();

    public Task<Result<Tenant>> GetAsync();

    public Task<Result> UpdateAsync(UpdateTenantParams @params);

    public Task<Result> VerifyEmailAsync();
}
