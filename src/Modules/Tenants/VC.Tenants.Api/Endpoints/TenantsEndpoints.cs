using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace VC.Tenants.Api.Endpoints;

public static class TenantsEndpoints
{
    public static IEndpointRouteBuilder AddTenantsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/tenants", CreateAsync);
        return builder;
    }

    private static Task CreateAsync(CreateTenantRequest request)
    {
        return Task.CompletedTask;
    }
}