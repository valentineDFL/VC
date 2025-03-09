using FluentResults;
using VC.Tenants.Application.Tenants.Models;

namespace VC.Tenants.Application.Tenants;

internal class TenantsService : ITenantsService
{
    public Task<Result> CreateAsync(CreateTenantParams @params)
    {
        throw new NotImplementedException();
    }
}
