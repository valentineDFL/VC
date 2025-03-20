using FluentResults;
using VC.Recources.Application.Models.Dto;

namespace VC.Recources.Application.Services;

public interface IResourceSevice
{
    public Task<Result> CreateResourceAsync(CreateResourceDto dto);

    public Task<Result<Resource.Domain.Entities.Resource>> GetResourceAsync(Guid resourceId);

    public Task<Result> UpdateResourceAsync(UpdateResourceDto dto);
}
