using FluentResults;
using VC.Recources.Application.Models.Dto;
using VC.Recources.Application.Models.Response;

namespace VC.Recources.Application.Services;

public interface IResourceSevice
{
    public Task<Result> CreateResourceAsync(CreateResourceDto dto);

    public Task<Result<ResourceDto>> GetResourceAsync(Guid resourceId);

    public Task<Result> UpdateResourceAsync(UpdateResourceDto dto);
}
