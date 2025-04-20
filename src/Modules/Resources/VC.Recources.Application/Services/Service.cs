using FluentResults;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Application.Interfaces;
using VC.Recources.Domain;
using VC.Recources.Domain.Entities;
using VC.Recources.Domain.UnitOfWork;
using VC.Utilities.Resolvers;

namespace VC.Recources.Application.Services;

public class Service(
    ITenantResolver _tenantResolver,
    IResourcesUnitOfWork _unitOfWork,
    IRepository _repository
)
    : IService
{
    public async Task<Result> AddAsync(CreateDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        var resource = dto.ToDomain();

        resource.Id = Guid.CreateVersion7();
        resource.TenantId = _tenantResolver.Resolve();

        await _repository.AddAsync(resource);
        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok();
    }

    public async Task<Result<Resource>> GetAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();

        var resource = await _repository.GetAsync(id);
        if (resource is null)
            return Result.Fail("Resource not found");

        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok(resource);
    }

    public async Task<Result> UpdateAsync(UpdateDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        var resource = await _repository.GetAsync(dto.Id);

        if (resource is null)
            return Result.Fail("Resource not found");

        var skills = dto.Skills
            .Select(s => new Skill(
                s.Name,
                new Experience(s.Experience.Years, s.Experience.Months)))
            .ToList();

        await resource.UpdateDetails(
            dto.Name,
            dto.Description,
            skills
        );

        _repository.Update(resource);
        await _unitOfWork.CommitTransactionAsync();

        return Result.Ok();
    }
}