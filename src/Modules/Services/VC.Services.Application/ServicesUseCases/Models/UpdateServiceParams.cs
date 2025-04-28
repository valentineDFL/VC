namespace VC.Services.Application.ServicesUseCases.Models;

public class UpdateServiceParams
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public TimeSpan Duration { get; set; }

    public Guid? CategoryId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<Guid> RequiredResources { get; set; } = [];
}
