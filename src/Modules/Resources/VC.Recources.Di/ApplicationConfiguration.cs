using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Recources.Application.Interfaces;
using VC.Recources.Application.Services;
using VC.Recources.Application.Validators;
using VC.Recources.Domain;
using VC.Recources.Infrastructure;

namespace VC.Recources.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureResourcesApplication(this IServiceCollection services)
    {
        services.AddScoped<IService, Service>();
        
        services.AddValidatorsFromAssemblyContaining<CreateResourceDtoValidator>(includeInternalTypes: true);
    }
}