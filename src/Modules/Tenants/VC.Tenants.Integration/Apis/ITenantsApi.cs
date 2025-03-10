using FluentResults;

namespace VC.Tenants.Integration.Apis;

public interface ITenantsApi
{
    Task<Result<string>> GetTenantNameAsync(string id); 
}
