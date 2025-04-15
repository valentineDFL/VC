using FluentResults;
using MediatR;
using VC.Services.Entities;
using VC.Services.Repositories;

namespace VC.Services.Application.Services.Queries.GetService;
public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, Result<Service>>
{
    private readonly IServicesRepository _dbRepository;

    public GetServiceQueryHandler(IServicesRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }

    public async Task<Result<Service>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var service = await _dbRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service == null)
            return Result.Fail<Service>("NotFound");

        return Result.Ok(service);
    }
}
