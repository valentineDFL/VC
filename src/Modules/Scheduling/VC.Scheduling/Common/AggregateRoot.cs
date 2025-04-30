namespace VC.Scheduling.Common;

public class AggregateRoot<TId> : IHasId<TId>
{
    public TId Id { get; set; }
}