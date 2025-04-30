namespace VC.Services.Common;

public abstract class DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}