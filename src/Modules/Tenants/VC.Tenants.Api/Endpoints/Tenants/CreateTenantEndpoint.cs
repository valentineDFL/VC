namespace VC.Tenants.Api.Endpoints.Tenants;

public static partial class TenantsEndpoints
{
    private static Task CreateAsync(CreateTenantRequest request)
    {
        return Task.CompletedTask;
    }
}

public record CreateTenantRequest(string Name);