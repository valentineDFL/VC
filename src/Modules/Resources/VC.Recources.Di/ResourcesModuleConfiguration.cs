using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Resources.Api.OpenApi;

namespace VC.Recources.Di;

public static class ResourcesModuleConfiguration
{
    public static void ConfigureResourceModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureResourcesInfrastructure(configuration);
        services.ConfigureResourcesApplication();
        services.ConfigureResourcesOpenApi();
    }

    public static IServiceCollection ConfigureResourcesOpenApi(this IServiceCollection services)
        => services.AddOpenApi(OpenApiConfig.DocumentName, opts => OpenApiConfig.ConfigureOpenApi(opts));
}
