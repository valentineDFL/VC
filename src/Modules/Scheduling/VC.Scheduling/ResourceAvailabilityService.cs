namespace VC.Scheduling;

/// <summary>
/// Реализация для модуля Resources
/// </summary>
public class ResourceAvailabilityService : IResourceAvailabilityService
{
    public Task<bool> IsResourceAvailableAsync(Guid resourceId, DateTime start, DateTime end,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}