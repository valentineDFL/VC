using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace VC.Tenants.Api.Endpoints.Tenants;

public static partial class TenantsEndpoints
{
    public static IEndpointRouteBuilder AddTenantsEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/tenants", CreateAsync);
        return builder;
    }
}
