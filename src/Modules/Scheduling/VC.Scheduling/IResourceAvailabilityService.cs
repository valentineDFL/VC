namespace VC.Scheduling;

/// <summary>
/// Сервис проверки доступности ресурсов
/// </summary>
public interface IResourceAvailabilityService
{
    Task<bool> IsResourceAvailableAsync(
        Guid resourceId,
        DateTime start,
        DateTime end,
        CancellationToken cancellationToken = default
    );
}