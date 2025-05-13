using VC.Core.Application.ServicesUseCases.Models;

namespace VC.Core.Application.ServicesUseCases;

public interface IGetAllServicesUseCase
{
    Task<List<ServiceDetailsDto>> ExecuteAsync();
}