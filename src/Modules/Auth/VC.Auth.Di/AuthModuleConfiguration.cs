using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Api.OpenApi;
using VC.Auth.Infrastructure.Persistence;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Di;

public static class AuthModuleConfiguration
{
    public static void ConfigureAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureServicesInfrastructure(configuration);
        services.ConfigureServicesApplication();
        services.ConfigureServicesOpenApi();

        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        var cookiesSettings = configuration.GetSection(nameof(CookiesSettings)).Get<CookiesSettings>();

        if (jwtSettings == null)
            throw new Exception("Jwt settings not found in configuration");
        
        if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey))
            throw new Exception("Jwt SecretKey is null or empty. Check appsettings.json");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidIssuer = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(securityKey.ToString())).ToString()
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[cookiesSettings.RememberMeCookieName];
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(x =>
            x.AddPolicy(Permissions.User, builder =>
                builder.Requirements
                    .Add(new PermissionRequirement(Permissions.User, Permissions.Tenant))));

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }

    private static IServiceCollection ConfigureServicesOpenApi(this IServiceCollection services)
        => services.AddOpenApi(
            OpenApiConfig.DocumentName,
            opts => OpenApiConfig.ConfigureOpenApi(opts));
}