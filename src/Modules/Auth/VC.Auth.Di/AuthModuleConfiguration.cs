using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Api.OpenApi;

namespace VC.Auth.Di;

public static class AuthModuleConfiguration
{
    public static void ConfigureAuthModule(this IServiceCollection services, IConfiguration configuration)
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