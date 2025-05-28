using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Api.Helpers;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Services;
using VC.Auth.Infrastructure;
using VC.Auth.Infrastructure.Persistence.DataContext;
using VC.Auth.Infrastructure.Persistence.Models;
using VC.Auth.Interfaces;

namespace VC.Auth.Di;

internal static class InfrastructureConfiguration
{
    public static void ConfigureServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("PostgresSQL");

        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString, o => o.MigrationsHistoryTable("__EFMigrationsHistory", "auth")));

        ConfigureInfrastructure(services, configuration);
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEncrypt, Encrypt>();
        services.AddScoped<IPasswordSaltGenerator, PasswordSaltGenerator>();

        services.AddScoped<IJwtOptions, JwtOptions>();
        services.AddScoped<IWebCookie, WebCookie>();
        services.AddScoped<IAuthService, AuthService>();

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<CookiesSettings>(configuration.GetSection("Cookies"));
    }
}