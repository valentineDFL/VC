using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VC.Core.Application;
using VC.Core.Application.EmployeeAvailableSlotsUseCases;
using VC.Core.Application.EmployeesUseCases;
using VC.Core.Application.Events;
using VC.Core.Application.ResourcesUseCases;
using VC.Core.Application.ResourcesUseCases.Validators;
using VC.Core.Application.ServicesUseCases;
using VC.Core.Application.WorkScheduleUseCases;
using VC.Core.Application.WorkScheduleUseCases.Models;
using VC.Core.Events;

namespace VC.Core.Di;

internal static class ApplicationConfiguration
{
    public static void ConfigureCoreApplication(this IServiceCollection services)
    {
        services.AddScoped<IResourcesService, ResourcesService>();
        services.AddScoped<ICreateServiceUseCase, CreateServiceUseCase>();
        services.AddScoped<IUpdateServiceUseCase, UpdateServiceUseCase>();

        services.AddScoped<IGetEmployeesUseCase, GetEmployeesUseCase>();
        services.AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>();

        services.ConfigureWorkScheduleApplication();
        
        services.AddScoped<IDomainEventHandler<ServiceCreatedEvent>,
            ServiceCreatedEventHandler>();
        
        services.AddValidatorsFromAssemblyContaining<CreateResourceDtoValidator>(includeInternalTypes: true);

        services.AddSingleton<OrderSnapshotFactory>();
    }
    
    public static void ConfigureWorkScheduleApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddWorkingHourExceptionUseCase, AddWorkingHourExceptionUseCase>();
        services.AddScoped<ICreateWorkScheduleUseCase, CreateWorkScheduleUseCase>();
        services.AddScoped<IGetWorkScheduleDetailsUseCase, GetWorkScheduleDetailsUseCase>();
        services.AddScoped<IGetEmployeeAvailableSlotsUseCase, GetEmployeeAvailableSlotsUseCase>();
    }
}