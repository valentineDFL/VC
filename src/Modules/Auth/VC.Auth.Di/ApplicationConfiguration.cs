using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Services;
using VC.Auth.Application.Validators;

namespace VC.Auth.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddValidatorsFromAssemblyContaining<SignUpValidator>(includeInternalTypes: true);
    }
}