using FluentResults;
using VC.Services.Application.ResourcesUseCases.Models;

namespace VC.Services.Application.ResourcesUseCases;

public interface IResourcesService
{
    Task<Result<Resource>> GetAsync(Guid id);
    Task<Result> CreateAsync(CreateResourceParams dto);

    Task<Result> UpdateAsync(UpdateResourceParams dto);
}