using Microsoft.Extensions.DependencyInjection;
using VC.Utilities.Resolvers;
using Microsoft.Extensions.Configuration;

namespace VC.Utilities;

public static class UtilitiesConfigurator
{
    public static void ConfigureUtilities(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITenantResolver, HttpContextTenantResolver>();
        services.AddScoped<IDateTime, SystemDateTimeProvider>();
    }
}