using FluentResults;
using MediatR;
using VC.Services.Entities;

namespace VC.Services.Application.Services.Queries.GetService;
public class GetServiceQuery : IRequest<Result<Service>>
{
    public Guid Id { get; set; }
}
