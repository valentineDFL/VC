using FluentResults;
using MediatR;
using VC.Services.Entities;
using VC.Services.Repositories;

namespace VC.Services.Application.Services.Queries.GetServiceList;
public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, Result<List<Service>>>
{
    private readonly IServicesRepository _dbRepository;
    
    public GetServiceListQueryHandler(IServicesRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<Result<List<Service>>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
    {
        var services = await _dbRepository.GetByTenantAsync(request.TenantId, cancellationToken);

        var serviceList = services.ToList();

        if (serviceList.Count == 0)
            return Result.Fail<List<Service>>("Not found");

        return Result.Ok(serviceList);
    }
}
