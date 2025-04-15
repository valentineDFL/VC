using FluentResults;
using MediatR;
using VC.Services.Application.Models;

namespace VC.Services.Application.Services.Queries.GetService;

public class GetServiceQuery : IRequest<Result<ServiceDto>>
{
    public Guid Id { get; set; }
}
