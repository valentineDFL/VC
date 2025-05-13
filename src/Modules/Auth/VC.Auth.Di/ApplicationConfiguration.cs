using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Infrastructure.Persistence.Repositories;
using VC.Auth.Repositories;

namespace VC.Auth.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}