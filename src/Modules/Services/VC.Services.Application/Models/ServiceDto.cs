using VC.Services.Entities;

namespace VC.Services.Application.Models;
public class ServiceDto(
    Guid id,
    string title,
    string description,
    decimal price,
    TimeSpan? duration,
    Category? category,
    bool isActive,
    DateTime createdAt,
    DateTime? updatedAt,
    List<Guid> resourceRequirement)
{
    public Guid Id { get; set; } = id;

    public string Title { get; set; } = title;

    public string Description { get; set; } = description;

    public decimal Price { get; set; } = price;

    public TimeSpan? Duration { get; set; } = duration;

    public Category? Category { get; set; } = category;

    public bool IsActive { get; set; } = isActive;

    public DateTime CreatedAt { get; set; } = createdAt;

    public DateTime? UpdatedAt { get; set; } = updatedAt;

    public List<Guid> ResourceRequirement { get; set; } = resourceRequirement;
}
