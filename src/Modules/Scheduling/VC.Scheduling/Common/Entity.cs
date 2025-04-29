namespace VC.Scheduling.Common;

public class Entity<TId> : IHasId<TId>
{
    public TId Id { get; set; }
}