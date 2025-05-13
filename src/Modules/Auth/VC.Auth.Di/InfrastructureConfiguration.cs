using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Api.Handlers;
using VC.Auth.Application.UseCases;
using VC.Auth.Infrastructure.Persistence;
using VC.Auth.Infrastructure.Persistence.Repositories;
using VC.Auth.Repositories;

namespace VC.Auth.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<AuthDatabaseContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresSQL")));

        ConfigureInfrastructure(services);
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IPasswordHashHandler, PasswordHashHandler>();
        services.AddScoped<IJwtService, JwtService>();
    }
}