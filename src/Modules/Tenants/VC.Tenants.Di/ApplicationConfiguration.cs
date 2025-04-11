using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Application.Tenants;

namespace VC.Tenants.Di;

internal static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureTenantsApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITenantsService, TenantsService>();

        return services;
    }
}