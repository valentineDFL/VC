using Microsoft.Extensions.DependencyInjection;

namespace VC.Utilities;

public static class UtilitiesConfigurator
{
    public static void ConfigureUtilities(this IServiceCollection services)
    {
        services.AddScoped<ITenantResolver, HttpContextTenantResolver>();
    }
}