using FluentResults;
using MediatR;
using VC.Services.Entities;

namespace VC.Services.Application.Services.Commands.CreateService;

public class CreateServiceCommand : IRequest<Result<Guid>>
{
    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public TimeSpan? Duration { get; set; }

    public Category? Category { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<Guid> ResourceRequirement { get; set; } = new();
}
