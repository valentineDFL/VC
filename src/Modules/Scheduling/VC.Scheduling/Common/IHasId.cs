namespace VC.Scheduling.Common;

public interface IHasId<TId>
{
    
    public TId Id { get; set; }
}