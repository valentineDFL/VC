using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Api.Helpers;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Services;
using VC.Auth.Infrastructure.Implementations;
using VC.Auth.Infrastructure.Implementations.Rabbit;
using VC.Auth.Infrastructure.Persistence.DataContext;
using VC.Auth.Infrastructure.Persistence.Repositories;
using VC.Auth.Interfaces;
using VC.Auth.Repositories;
using VC.Shared.RabbitMQIntegration.Consumers.Interfaces;
using VC.Shared.Utilities.Options.Jwt;

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
        services.Configure<CookiesSettings>(configuration.GetSection(nameof(CookiesSettings)));
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        services.AddScoped<IEncrypter, Encrypt>();
        services.AddScoped<IPasswordSaltGenerator, PasswordSaltGenerator>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IWebCookie, WebCookie>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IJwtClaimsGenerator, JwtClaimsGenerator>();

        services.AddSingleton<IConsumer, CreatedTenantsConsumer>();
    }
}