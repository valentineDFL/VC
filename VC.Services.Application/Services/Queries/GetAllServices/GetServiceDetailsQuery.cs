using MediatR;
using VC.Services.Entities;

namespace VC.Services.Application.Services.Queries.GetAllServices;
public class GetServiceDetailsQuery : IRequest<Service>
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }
}
