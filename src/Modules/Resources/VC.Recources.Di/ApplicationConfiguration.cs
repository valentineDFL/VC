using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Recources.Application.Services;
using VC.Recources.Application.Validators;

namespace VC.Recources.Di;

public static class ApplicationConfiguration
{
    public static void ConfigureResourcesApplication(this IServiceCollection services)
    {
        services.AddScoped<IResourceService, ResourceService>();
        
        services.AddValidatorsFromAssemblyContaining<CreateResourceDtoValidator>(includeInternalTypes: true);
    }
}