using FluentResults;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Domain.Entities;
using VC.Recources.UnitOfWork;
using VC.Utilities.Resolvers;

namespace VC.Recources.Application.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly ITenantResolver _tenantResolver;
    private readonly IDbSaver _dbSaver;

    public ResourceService(
        IResourceRepository resourceRepository,
        ITenantResolver tenantResolver,
        IDbSaver dbSaver
    )
    {
        _resourceRepository = resourceRepository;
        _tenantResolver = tenantResolver;
        _dbSaver = dbSaver;
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

        return resource is null ? Result.Fail("Resource not found") : Result.Ok(resource);
    }

    public async Task<Result> UpdateResourceAsync(UpdateResourceDto dto)
    {
        var resource = await _resourceRepository.GetAsync(dto.Id);
        
        resource.TenantId = _tenantResolver.Resolve();
        
        _resourceRepository.Update(resource);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }
}