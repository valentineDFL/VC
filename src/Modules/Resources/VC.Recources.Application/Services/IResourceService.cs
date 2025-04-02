using FluentResults;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Services;

public interface IResourceService
{
    public Task<Result> CreateResourceAsync(CreateResourceDto dto);

    public Task<Result<Resource>> GetResourceAsync(Guid id);

    public Task<Result> UpdateResourceAsync(UpdateResourceDto dto);
}
