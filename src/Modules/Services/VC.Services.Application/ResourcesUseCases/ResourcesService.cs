using FluentResults;
using VC.Services.Application.ResourcesUseCases.Models;
using VC.Services.Repositories;
using VC.Utilities.Resolvers;

namespace VC.Services.Application.ResourcesUseCases;

public class ResourcesService(
    ITenantResolver _tenantResolver,
    IUnitOfWork _unitOfWork) : IResourcesService
{
    public async Task<Result<Resource>> GetAsync(Guid id)
    {
        var resource = await _unitOfWork.Resources.GetByIdAsync(id);
        if (resource is null)
            return Result.Fail("Resource not found");

        return Result.Ok(resource);
    }

    public async Task<Result> CreateAsync(CreateResourceParams dto)
    {
        var resource = new Resource(Guid.CreateVersion7(), _tenantResolver.Resolve())
        {
            Title = dto.Title,
            Description = dto.Description
        };

        await _unitOfWork.Resources.AddAsync(resource);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(UpdateResourceParams dto)
    {
        var resource = await _unitOfWork.Resources.GetByIdAsync(dto.Id);

        if (resource is null)
            return Result.Fail("Resource not found");

        resource.Title = dto.Title;
        resource.Description = dto.Description;
        
        await _unitOfWork.Resources.UpdateAsync(resource);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }
}