using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Core.Application.EmployeesUseCases;
using VC.Core.Application.Events;
using VC.Core.Application.ResourcesUseCases;
using VC.Core.Application.ResourcesUseCases.Validators;
using VC.Core.Application.ServicesUseCases;
using VC.Core.Application.WorkScheduleUseCases;
using VC.Core.Events;

namespace VC.Core.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureServicesApplication(this IServiceCollection services)
    {
        services.AddScoped<IResourcesService, ResourcesService>();
        services.AddScoped<ICreateServiceUseCase, CreateServiceUseCase>();

        services.AddScoped<IGetEmployeesUseCase, GetEmployeesUseCase>();
        services.AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>();

        services.ConfigureWorkScheduleApplication();
        
        services.AddScoped<IDomainEventHandler<ServiceCreatedEvent>,
            ServiceCreatedEventHandler>();
        
        services.AddValidatorsFromAssemblyContaining<CreateResourceDtoValidator>(includeInternalTypes: true);
    }
    
    public static void ConfigureWorkScheduleApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateWorkScheduleUseCase, CreateWorkScheduleUseCase>();
        services.AddScoped<ICreateWorkScheduleUseCase, CreateWorkScheduleUseCase>();
        services.AddScoped<IAddWorkingHourExceptionUseCase, AddWorkingHourExceptionUseCase>();
        services.AddScoped<ICreateWorkScheduleUseCase, CreateWorkScheduleUseCase>();
        services.AddScoped<IGetWorkScheduleDetailsUseCase, GetWorkScheduleDetailsUseCase>();
    }
}