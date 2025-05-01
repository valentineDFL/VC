using FluentResults;
using VC.Services.Repositories;

namespace VC.Services.Application.ServicesUseCases;

public interface IRemoveServiceUseCase
{
    Task<Result<Guid>> ExecuteAsync(Guid id, CancellationToken cancellationToken = default);
}

public class RemoveServiceUseCase(IUnitOfWork _unitOfWork) : IRemoveServiceUseCase
{
    public async Task<Result<Guid>> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.Services.GetByIdAsync(id, cancellationToken);
        if (service is null)
            return Result.Fail("Service not found.");
        
        await _unitOfWork.Services.RemoveAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(service.Id);
    }
}