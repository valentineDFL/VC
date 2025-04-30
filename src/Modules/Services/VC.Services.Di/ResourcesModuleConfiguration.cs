using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Services.Api.OpenApi;

namespace VC.Services.Di;

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