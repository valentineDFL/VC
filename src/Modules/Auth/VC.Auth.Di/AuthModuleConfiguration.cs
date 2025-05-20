using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VC.Auth.Api.OpenApi;
using VC.Auth.Interfaces;

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
                        Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"])).ToString()
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        /*services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //options.DefaultChallengeScheme = ;
        });*/
        services.AddAuthorization();
    }

    private static IServiceCollection ConfigureServicesOpenApi(this IServiceCollection services)
        => services.AddOpenApi(
            OpenApiConfig.DocumentName,
            opts => OpenApiConfig.ConfigureOpenApi(opts));
}