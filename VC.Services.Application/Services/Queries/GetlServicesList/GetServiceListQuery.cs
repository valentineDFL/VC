using FluentResults;
using MediatR;
using VC.Services.Entities;

namespace VC.Services.Application.Services.Queries.GetServiceList;
public class GetServiceListQuery : IRequest<Result<List<Service>>>
{
    public Guid TenantId { get; set; }
}
