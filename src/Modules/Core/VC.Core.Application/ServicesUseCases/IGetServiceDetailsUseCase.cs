using VC.Core.Application.ServicesUseCases.Models;

namespace VC.Core.Application.ServicesUseCases;

public interface IGetServiceDetailsUseCase
{
    Task<ServiceDetailsDto?> ExecuteAsync(Guid serviceId);
}