using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VC.Integrations.Di;

public static class IntergrationModuleConfigurator
{
    public static void ConfigureIntegrationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMailkitIntergration(configuration);
    }
}