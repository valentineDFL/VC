using MediatR;
using VC.Services.Entities;

namespace VC.Services.Application.Services.Commands.CreateService;
public class CreateServiceCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public TimeSpan? Duration { get; set; }

    public Category? Category { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid TenantId { get; set; }

    public List<Guid>? ResourceRequirement { get; set; }
}
