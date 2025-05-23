using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Api.OpenApi;
using VC.Auth.Constants;
using VC.Auth.Infrastructure.Persistence;

namespace VC.Auth.Di;

public static class AuthModuleConfiguration
{
    public static void ConfigureAuthModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureServicesInfrastructure(configuration);
        services.ConfigureServicesApplication();
        services.ConfigureServicesOpenApi();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])).ToString()
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[AuthConstants.RememberMeCookieName];
                        return Task.CompletedTask;
                    }
                };
            });
        
        services.AddAuthorization(x =>
            x.AddPolicy(Permissions.User, builder =>
                builder.Requirements
                    .Add(new PermissionRequirements(Permissions.User, Permissions.Tenant))));
        
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }

    private static IServiceCollection ConfigureServicesOpenApi(this IServiceCollection services)
        => services.AddOpenApi(
            OpenApiConfig.DocumentName,
            opts => OpenApiConfig.ConfigureOpenApi(opts));
}