using Microsoft.AspNetCore.Http;
using VC.Utilities;
using VC.Tenants.Api.Endpoints.Tenants.Models;
using VC.Tenants.Application.Tenants;

namespace VC.Tenants.Api.Endpoints.Tenants;

public static partial class TenantsEndpoints
{
    private static async Task<IResult> CreateAsync(CreateTenantRequest request, ITenantsService tenantsService)
    {
        var result = await tenantsService.CreateAsync(request.ToCreateTenantParams());

        return result.ToMinimalApi();
    }
}