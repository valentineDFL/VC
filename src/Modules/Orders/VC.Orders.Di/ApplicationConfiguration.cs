using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VC.Orders.Di;

internal static class ApplicationConfiguration
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}