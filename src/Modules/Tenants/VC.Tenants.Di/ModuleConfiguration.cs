using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Tenants.Api.Endpoints.Tenants;
using VC.Tenants.Api.OpenApi;
using VC.Utilities;

namespace VC.Tenants.Di;

public static class ModuleConfiguration
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationConfiguration.ConfigureTenantsApplication(services, configuration);
        return services
            .ConfigureOpenApi(configuration);
    }
    
    public static IServiceCollection ConfigureOpenApi(this IServiceCollection services, IConfiguration configuration)
        => services.AddOpenApi(OpenApiConfig.DocumentName, opts => OpenApiConfig.ConfigureOpenApi(opts));
    
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        =>  builder.AddTenantsEndpoints();

    public static void ConfigureTenantsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureTenantsApiExtensions();
        services.ConfigureTenantsApplication(configuration);
        services.ConfigureTenantsInfrastructure(configuration);
        services.Configure<TenantsModuleSettings>(configuration.GetSection(nameof(TenantsModuleSettings)));
    }
}
