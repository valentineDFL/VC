namespace VC.Orders.Common;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private readonly List<DomainEvent> _domainEvents = [];

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(TId id) : base(id) { }

    protected void AddDomainEvent(DomainEvent domainEvent) 
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() 
        => _domainEvents.Clear();
}