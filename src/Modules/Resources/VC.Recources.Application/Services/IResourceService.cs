using FluentResults;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Services;

public interface IResourceService
{
    public Task<Result> CreateAsync(CreateResourceDto dto);

    public Task<Result<Resource>> GetAsync(Guid id);

    public Task<Result> UpdateAsync(UpdateResourceDto dto);
}
