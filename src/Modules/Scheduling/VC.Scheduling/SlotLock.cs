namespace VC.Scheduling;

public class SlotLock : Entity<Guid>
{
    public Guid SlotId { get; }
    public Guid TenantId { get; }
    public DateTime ExpiryTime { get; private set; }
    public LockReason Reason { get; }

    public SlotLock(Guid slotId, Guid tenantId, TimeSpan duration, LockReason reason)
    {
        SlotId = slotId;
        TenantId = tenantId;
        ExpiryTime = DateTime.UtcNow.Add(duration);
        Reason = reason;
    }

    public bool IsExpired => DateTime.UtcNow >= ExpiryTime;

    public void Extend(TimeSpan extension)
    {
        ExpiryTime = ExpiryTime.Add(extension);
    }
}