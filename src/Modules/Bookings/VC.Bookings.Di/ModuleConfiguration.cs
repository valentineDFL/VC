using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Bookings.Api.Endpoints.Bookings;
using VC.Bookings.Api.OpenApi;

namespace VC.Bookings.Di;

public static class ModuleConfiguration
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationConfiguration.Configure(services, configuration);
        return services
            .ConfigureOpenApi(configuration);
    }

    public static IServiceCollection ConfigureOpenApi(this IServiceCollection services, IConfiguration configuration)
        => services.AddOpenApi(OpenApiConfig.DocumentName, opts => OpenApiConfig.ConfigureOpenApi(opts));
    
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        =>  builder.AddBookingsEndpoints();
}
