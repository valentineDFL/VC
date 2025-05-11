using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Core.Api.OpenApi;

namespace VC.Core.Di;

public static class ResourcesModuleConfiguration
{
    public static void ConfigureServicesModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureServicesInfrastructure(configuration);
        services.ConfigureServicesApplication();
        services.ConfigureServicesOpenApi();
    }

    private static IServiceCollection ConfigureServicesOpenApi(this IServiceCollection services)
        => services.AddOpenApi(
            OpenApiConfig.DocumentName,
            opts => OpenApiConfig.ConfigureOpenApi(opts));
}