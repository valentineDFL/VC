using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Services.Application.Events;
using VC.Services.Application.ResourcesUseCases;
using VC.Services.Application.ResourcesUseCases.Validators;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Events;

namespace VC.Services.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IResourcesService, ResourcesService>();
        services.AddScoped<ICreateServiceUseCase, CreateServiceUseCase>();
        
        services.AddScoped<IDomainEventHandler<ServiceCreatedEvent>,
            ServiceCreatedEventHandler>();
        
        services.AddValidatorsFromAssemblyContaining<CreateResourceDtoValidator>(includeInternalTypes: true);
    }
}