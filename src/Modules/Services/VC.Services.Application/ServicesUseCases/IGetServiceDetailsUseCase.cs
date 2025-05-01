using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Application.ServicesUseCases;

public interface IGetServiceDetailsUseCase
{
    Task<ServiceDetailsDto?> ExecuteAsync(Guid serviceId);
}