using FluentResults;
using VC.Tenants.Integration.Apis;

namespace VC.Tenants.Application.Tenants;

internal class TenantsApi : ITenantsApi
{
    public Task<Result<string>> GetTenantNameAsync(string id)
    {
        throw new NotImplementedException();
    }
}