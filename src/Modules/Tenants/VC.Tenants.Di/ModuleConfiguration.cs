using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Api.Endpoints;
using VC.Tenants.Api.Endpoints.Tenants;

namespace VC.Tenants.Di;

public static class ModuleConfiguration
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationConfiguration.Configure(services, configuration);
        return services;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        =>  builder.AddTenantsEndpoints();
}
