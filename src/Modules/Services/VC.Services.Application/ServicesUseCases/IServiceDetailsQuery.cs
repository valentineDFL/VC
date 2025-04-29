using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Application.ServicesUseCases;

public interface IServiceDetailsQuery
{
    Task<ServiceDetailsDto?> ExecuteAsync(Guid serviceId);
}