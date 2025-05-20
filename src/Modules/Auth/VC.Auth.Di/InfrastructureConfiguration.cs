using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Api.Handlers;
using VC.Auth.Application;
using VC.Auth.Infrastructure;
using VC.Auth.Infrastructure.Persistence;
using VC.Auth.Infrastructure.Persistence.Models;
using VC.Auth.Interfaces;

namespace VC.Auth.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<AuthDatabaseContext>(options => options.UseNpgsql(connectionString));

        ConfigureInfrastructure(services, configuration);
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordHashHandler, PasswordHashHandler>();

        services.AddSingleton<JwtSettings>(_ => new JwtSettings
        {
            SecretKey = configuration["JwtOptions:SecretKey"]!,
            ExpiresTime = configuration.GetValue<int>("JwtOptions:ExpiresTime")
        });
        
        services.AddScoped<IJwtOptions, JwtOptions>();
        services.AddScoped<IUserService, UserService>();
    }
}