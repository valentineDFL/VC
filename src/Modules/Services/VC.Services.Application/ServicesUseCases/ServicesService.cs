using FluentResults;
using VC.Services.Application.ServicesUseCases.Mapping;
using VC.Services.Application.ServicesUseCases.Models;
using VC.Services.Repositories;
using VC.Utilities.Resolvers;

namespace VC.Services.Application.ServicesUseCases;

public class ServicesService(ITenantResolver _tenantResolver, IUnitOfWork _unitOfWork) : IServicesService
{
    public async Task<Result<List<ServiceDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var services = await _unitOfWork.Services.GetByTenantAsync(_tenantResolver.Resolve(), cancellationToken);

        return Result.Ok(MapperToDto.ConvertToDtoList(services));
    }
    
    public async Task<Result<ServiceDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.Services.GetByIdAsync(id, cancellationToken);

        if (service == null)
            return Result.Fail<ServiceDto>("NotFound");

        var serviceDto = MapperToDto.ConvertToDto(service);

        return Result.Ok(serviceDto);
    }
    
    public async Task<Result<Guid>> CreateAsync(CreateServiceParams parameters, CancellationToken cancellationToken)
    {
        var service = new Service(Guid.CreateVersion7(), _tenantResolver.Resolve())
        {
            Title = parameters.Title,
            Description = parameters.Description,
            BasePrice = parameters.Price,
            BaseDuration = parameters.Duration,
            CategoryId = parameters.CategoryId,
            IsActive = parameters.IsActive,
            CreatedAt = DateTime.UtcNow,
            RequiredResources = parameters.RequiredResources
        };

        await _unitOfWork.Services.AddAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(service.Id);
    }
    
    public async Task<Result> UpdateAsync(UpdateServiceParams parameters, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.Services.GetByIdAsync(parameters.Id, cancellationToken);
        if (service is null)
            return Result.Fail("Not found");

        service.Title = parameters.Title;
        service.Description = parameters.Description;
        service.BasePrice = parameters.Price;
        service.BaseDuration = parameters.Duration;
        service.CategoryId = parameters.CategoryId;
        service.UpdatedAt = DateTime.UtcNow;
        service.IsActive = parameters.IsActive;
        service.RequiredResources = parameters.RequiredResources;

        await _unitOfWork.Services.UpdateAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
    
    public async Task<Result> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var service = await _unitOfWork.Services.GetByIdAsync(id, cancellationToken);
        if (service is null)
            return Result.Fail("Not found service");

        await _unitOfWork.Services.RemoveAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}