using FluentResults;
using VC.Services.Application.ResourcesUseCases.Models;
using VC.Services.Repositories;
using VC.Utilities.Resolvers;

namespace VC.Services.Application.ResourcesUseCases;

public class ResourcesService(
    ITenantResolver _tenantResolver,
    IUnitOfWork _unitOfWork) : IResourcesService
{
    public async Task<Result<Resource?>> GetAsync(Guid id)
    {
        return await _unitOfWork.Resources.GetByIdAsync(id);
    }

    public async Task<Result> CreateAsync(CreateResourceParams parameters)
    {
        var resource = new Resource(
            Guid.CreateVersion7(),
            _tenantResolver.Resolve(),
            parameters.Title,
            parameters.Count)
        {
            Description = parameters.Description
        };

        await _unitOfWork.Resources.AddAsync(resource);
        await _unitOfWork.CommitAsync();
        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(UpdateResourceParams parameters)
    {
        var resource = await _unitOfWork.Resources.GetByIdAsync(parameters.Id);
        if (resource is null)
            return Result.Fail("Resource not found");

        resource.Title = parameters.Title;
        resource.Description = parameters.Description;
        resource.Count = parameters.Count;
        
        await _unitOfWork.Resources.UpdateAsync(resource);
        await _unitOfWork.CommitAsync();
        return Result.Ok();
    }
    
    public async Task<Result> RemoveAsync(Guid id)
    {
        var resource = await _unitOfWork.Resources.GetByIdAsync(id);
        if (resource is null)
            return Result.Fail("Resource not found");
        
        await _unitOfWork.Resources.DeleteAsync(resource);
        await _unitOfWork.CommitAsync();
        return Result.Ok();
    }
}