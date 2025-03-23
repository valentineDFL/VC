using FluentResults;
using VC.Recources.Application.Endpoints.Models.Request;
using VC.Recources.Domain.Entities;
using VC.Recources.UnitOfWork;
using VC.Utilities.Resolvers;

namespace VC.Recources.Application.Services;

public class ResourceService : IResourceSevice
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IDbSaver _dbSaver;
    private readonly ITenantResolver _tenantResolver;

    public ResourceService(
        IResourceRepository resourceRepository,
        IDbSaver dbSaver,
        ITenantResolver tenantResolver
        )
    {
        _resourceRepository = resourceRepository;
        _dbSaver = dbSaver;
        _tenantResolver = tenantResolver;
    }

    public async Task<Result> CreateResourceAsync(CreateResourceDto dto)
    {
        var resource = dto.ToResourceDomain();
        resource.Id = Guid.CreateVersion7();
        resource.TenantId = _tenantResolver.Resolve();

        await _resourceRepository.AddAsync(resource);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }

    public async Task<Result<Resource>> GetResourceAsync(Guid id)
    {
        var resource = await _resourceRepository.GetAsync(id);

        if (resource is null)
            return Result.Fail("Resource not found .!.");

        return Result.Ok(resource);
    }

    public async Task<Result> UpdateResourceAsync(UpdateResourceDto dto)
    {
        var resource = dto.ToResourceDomain();

        resource.TenantId = _tenantResolver.Resolve();

        _resourceRepository.Update(resource);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }
}
