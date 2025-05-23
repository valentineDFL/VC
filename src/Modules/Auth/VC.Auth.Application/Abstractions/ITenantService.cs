using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application.Abstractions;

public interface ITenantService
{
    Task CreateServiceAsync(CreateServiceRequest request);
    
    Task GetServiceAsync(Guid id);
    
    Task UpdateScheduleAsync(UpdateScheduleRequest request);
}