using FluentResults;
using VC.Recources.Application.Endpoints.Models.Request;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Services;

public interface IResourceSevice
{
    public Task<Result> CreateResourceAsync(CreateResourceDto dto);

    public Task<Result<Resource>> GetResourceAsync(Guid id);

    public Task<Result> UpdateResourceAsync(UpdateResourceDto dto);
}
