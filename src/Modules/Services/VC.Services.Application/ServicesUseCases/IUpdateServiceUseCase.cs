using FluentResults;
using VC.Services.Application.ServicesUseCases.Models;
using VC.Services.Repositories;

namespace VC.Services.Application.ServicesUseCases;

public interface IUpdateServiceUseCase
{
    Task<Result<Guid>> ExecuteAsync(UpdateServiceParams parameters, CancellationToken cancellationToken = default);
}

public class UpdateServiceUseCase(IUnitOfWork _unitOfWork) : IUpdateServiceUseCase
{
    public async Task<Result<Guid>> ExecuteAsync(UpdateServiceParams parameters, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.Services.GetByIdAsync(parameters.Id, cancellationToken);
        if (service is null)
            return Result.Fail("Service not found.");

        if (service.Title != parameters.Title && await _unitOfWork.Services.ExistsAsync(parameters.Title))
            return Result.Fail("Service with this title already exists.");

        if (parameters.RequiredResources != null)
        {
            var resources = await _unitOfWork.Resources.GetByIdsAsync(
                parameters.RequiredResources, cancellationToken);

            if (resources.Count != parameters.RequiredResources.Count)
                return Result.Fail("One or more resources do not exist.");
        }
        
        service.Title = parameters.Title;
        service.Description = parameters.Description;
        service.BasePrice = parameters.BasePrice;
        service.BaseDuration = parameters.BaseDuration;
        service.CategoryId = parameters.CategoryId;
        
        service.RemoveAllResources();
        foreach (var resourceId in parameters.RequiredResources ?? [])
            service.AddResource(resourceId);

        var providedEmployeeIds = parameters.EmployeeAssignments?.Select(e => e.EmployeeId).ToHashSet() ?? [];
        var toRemove = service.EmployeeAssignments
            .Where(ea => !providedEmployeeIds.Contains(ea.EmployeeId))
            .ToList();
        
        foreach (var assignment in toRemove)
            service.RemoveEmployeeAssignment(assignment);
        
        if (parameters.EmployeeAssignments != null)
        {
            foreach (var dto in parameters.EmployeeAssignments)
            {
                service.AssignEmployee(
                    dto.EmployeeId,
                    dto.Price,
                    dto.Duration
                );
            }
        }

        await _unitOfWork.Services.UpdateAsync(service, cancellationToken);
        await _unitOfWork.CommitAsync();
        return Result.Ok(service.Id);
    }
}