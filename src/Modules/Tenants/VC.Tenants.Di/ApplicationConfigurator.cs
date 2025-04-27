using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Application;
using VC.Tenants.Application.Tenants;

namespace VC.Tenants.Di;

internal static class ApplicationConfigurator
{
    public static IServiceCollection ConfigureTenantsApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITenantsService, TenantsService>();
        services.AddScoped<ISlugGenerator, ByNameSlugGenerator>();
        services.AddScoped<IEmailVerifyCodeGenerator, DyDateCodeGenerator>();
        services.AddSingleton<ITEnantEmailVerificationFormFactory, TenantEmailVerifyMessagesFactory>();

        return services;
    }
}