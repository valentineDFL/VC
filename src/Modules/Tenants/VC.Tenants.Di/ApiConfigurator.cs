using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Api.Models.Request.Tenant;
using VC.Tenants.Api.Validation;
using VC.Utilities;

namespace VC.Tenants.Di;

internal static class ApiConfigurator
{
    public static void ConfigureTenantsApiExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<CreateTenantRequest>, CreateTenantValidation>();
        services.AddScoped<IValidator<UpdateTenantRequest>, UpdateTenantValidation>();
        services.Configure<EndpointsUrls>(configuration.GetSection(nameof(EndpointsUrls)));
    }
}