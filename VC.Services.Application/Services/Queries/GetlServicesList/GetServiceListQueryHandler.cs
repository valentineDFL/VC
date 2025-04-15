using FluentResults;
using MediatR;
using VC.Services.Application.Mapping;
using VC.Services.Application.Models;
using VC.Services.Repositories;

namespace VC.Services.Application.Services.Queries.GetServiceList;

public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, Result<List<ServiceDto>>>
{
    private readonly IServicesRepository _dbRepository;
    
    public GetServiceListQueryHandler(IServicesRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<Result<List<ServiceDto>>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
    {
        var services = await _dbRepository.GetByTenantAsync(request.TenantId, cancellationToken);

        var serviceList = services.ToList();

        if (serviceList.Count == 0)
            return Result.Fail<List<ServiceDto>>("Not found");

        var serviceDtoList = MapperToDto.ConvertToDtoList(serviceList);

        return Result.Ok(serviceDtoList);
    }
}
