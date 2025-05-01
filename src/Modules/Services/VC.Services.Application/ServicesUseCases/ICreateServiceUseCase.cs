using FluentResults;
using VC.Services.Application.ServicesUseCases.Models;
using VC.Services.Repositories;
using VC.Utilities.Resolvers;

namespace VC.Services.Application.ServicesUseCases;

public interface ICreateServiceUseCase
{
    Task<Result<Guid>> ExecuteAsync(CreateServiceParams parameters, CancellationToken cancellationToken = default);
}

public class CreateServiceUseCase(
    ITenantResolver _tenantResolver,
    IUnitOfWork _unitOfWork) : ICreateServiceUseCase
{
    public async Task<Result<Guid>> ExecuteAsync(CreateServiceParams parameters, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Services.ExistsAsync(parameters.Title))
            return Result.Fail<Guid>("Service with this title already exists.");

        var resources =
            await _unitOfWork.Resources.GetByIdsAsync(parameters.RequiredResources ?? [], cancellationToken);
        if (resources.Count != (parameters.RequiredResources?.Count ?? 0))
            return Result.Fail<Guid>("One or more resources do not exist.");

        var service = new Service(
            Guid.CreateVersion7(),
            _tenantResolver.Resolve(),
            parameters.Title,
            parameters.BasePrice,
            parameters.BaseDuration) { Description = parameters.Description, CategoryId = parameters.CategoryId };

        foreach (var resourceId in parameters.RequiredResources ?? [])
            service.AddResource(resourceId);

        foreach (var assignmentDto in parameters.EmployeeAssignments ?? [])
        {
            service.AssignEmployee(
                assignmentDto.EmployeeId,
                assignmentDto.Price,
                assignmentDto.Duration
            );
        }

        await _unitOfWork.Services.AddAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(service.Id);
    }
}