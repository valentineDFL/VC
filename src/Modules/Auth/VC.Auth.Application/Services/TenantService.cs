using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models.Requests;

namespace VC.Auth.Application.Services;

//TODO: реализовать TenantService
public class TenantService : ITenantService
{
    public Task CreateServiceAsync(CreateServiceRequest request)
    {
        throw new NotImplementedException();
    }

    public Task GetServiceAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateScheduleAsync(UpdateScheduleRequest request)
    {
        throw new NotImplementedException();
    }
}