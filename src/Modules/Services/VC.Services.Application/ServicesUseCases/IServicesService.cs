using FluentResults;
using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Application.ServicesUseCases;

public interface IServicesService
{
    Task<Result<List<ServiceDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ServiceDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Guid>> CreateAsync(CreateServiceParams parameters, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(UpdateServiceParams parameters, CancellationToken cancellationToken = default);
    Task<Result> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}