using FluentResults;
using VC.Core.Application.ResourcesUseCases.Models;
using VC.Core.Services;

namespace VC.Core.Application.ResourcesUseCases;

public interface IResourcesService
{
    Task<Result<Resource?>> GetAsync(Guid id);
    Task<Result> CreateAsync(CreateResourceParams parameters);

    Task<Result> UpdateAsync(UpdateResourceParams parameters);
    Task<Result> RemoveAsync(Guid id);
}