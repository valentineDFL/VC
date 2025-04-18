namespace VC.Scheduling;

/// <summary>
/// Контекст, необходимых при проверке доступности временных слотов.
/// </summary>
public class SchedulingContext
{
    public Guid TenantId { get; }

    public SchedulingContext(
        Guid tenantId)
    {
        TenantId = tenantId;
    }
}