using FluentResults;
using MediatR;
using VC.Services.Application.Mapping;
using VC.Services.Application.Models;
using VC.Services.Repositories;

namespace VC.Services.Application.Services.Queries.GetService;

public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, Result<ServiceDto>>
{
    private readonly IServicesRepository _dbRepository;

    public GetServiceQueryHandler(IServicesRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<Result<ServiceDto>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var service = await _dbRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service == null)
            return Result.Fail<ServiceDto>("NotFound");

        var serviceDto = MapperToDto.ConvertToDto(service);

        return Result.Ok(serviceDto);
    }
}
