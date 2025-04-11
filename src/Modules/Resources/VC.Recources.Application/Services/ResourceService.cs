using FluentResults;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Domain;
using VC.Recources.Domain.Entities;
using VC.Recources.Domain.UnitOfWork;
using VC.Utilities.Resolvers;

namespace VC.Recources.Application.Services;

public class ResourceService(
    IResourceRepository _resourceRepository,
    ITenantResolver _tenantResolver,
    IDbSaver _dbSaver,
    INameUniquenessChecker _nameUniquenessChecker)
    : IResourceService
{
    public async Task<Result> CreateAsync(CreateResourceDto dto)
    {
        var resource = dto.ToResourceDomain();

        resource.Id = Guid.CreateVersion7();
        resource.TenantId = _tenantResolver.Resolve();

        await _resourceRepository.AddAsync(resource);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result<Resource>> GetAsync(Guid id)
    {
        var resource = await _resourceRepository.GetAsync(id);

        return resource is null ? Result.Fail("Resource not found") : Result.Ok(resource);
    }

    public async Task<Result> UpdateAsync(UpdateResourceDto dto)
    {
        var resource = await _resourceRepository.GetAsync(dto.Id);

        if (resource is null)
            return Result.Fail("Resource not found");

        var skills = dto.Skills
            .Select(s => new Skill(
                s.Name,
                new Experience(s.Experience.Years, s.Experience.Months)))
            .ToList();

        await resource.UpdateDetails(
            dto.Name,
            _nameUniquenessChecker,
            dto.Description,
            skills
        );

        _resourceRepository.Update(resource);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }
}