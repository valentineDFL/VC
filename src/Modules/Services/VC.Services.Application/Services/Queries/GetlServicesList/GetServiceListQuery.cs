using FluentResults;
using MediatR;
using VC.Services.Application.Models;

namespace VC.Services.Application.Services.Queries.GetServiceList;

public class GetServiceListQuery : IRequest<Result<List<ServiceDto>>>
{
    public Guid TenantId { get; set; }
}
