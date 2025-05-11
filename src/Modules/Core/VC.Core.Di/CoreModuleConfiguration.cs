using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Core.Api.OpenApi;

namespace VC.Core.Di;

public static class CoreModuleConfiguration
{
    public static void ConfigureCoreModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureCoreInfrastructure(configuration);
        services.ConfigureCoreApplication();
        services.ConfigureCoreOpenApi();
        services.ConfigureDomain();
    }

    private static IServiceCollection ConfigureCoreOpenApi(this IServiceCollection services)
        => services.AddOpenApi(
            OpenApiConfig.DocumentName,
            opts => OpenApiConfig.ConfigureOpenApi(opts));
}