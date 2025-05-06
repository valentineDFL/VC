using FluentResults;
using VC.Services.Application.ResourcesUseCases.Models;

namespace VC.Services.Application.ResourcesUseCases;

public interface IResourcesService
{
    Task<Result<Resource?>> GetAsync(Guid id);
    Task<Result> CreateAsync(CreateResourceParams parameters);

    Task<Result> UpdateAsync(UpdateResourceParams parameters);
    Task<Result> RemoveAsync(Guid id);
}