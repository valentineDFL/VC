using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Application;

namespace VC.Auth.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEncrypt, Encrypt>();
    }
}