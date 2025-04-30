namespace VC.Scheduling.BusinessRules;

/// <summary>
/// Это бизнес-правило проверяет, что для бронирования основного ресурса доступны все зависимые ресурсы в тот же временной интервал.
/// </summary>
/// <remarks>
/// Сценарий: Для бронирования стоматолога (основной ресурс) требуется рентген-аппарат (зависимый ресурс).
/// </remarks>
public sealed class ResourceDependencyRule : BusinessRule
{
    private readonly Guid _mainResourceId;
    private readonly IReadOnlyList<Guid> _dependentResourceIds;
    private readonly IResourceAvailabilityService _availabilityService;

    public ResourceDependencyRule(
        Guid mainResourceId,
        ICollection<Guid> dependentResourceIds,
        IResourceAvailabilityService availabilityService)
    {
        if (dependentResourceIds == null || dependentResourceIds.Count == 0)
            throw new DomainException("Dependent resources required", "INVALID_DEPENDENCIES");
        
        _mainResourceId = mainResourceId;
        _dependentResourceIds = dependentResourceIds.ToList();
        _availabilityService = availabilityService 
                               ?? throw new ArgumentNullException(nameof(availabilityService));
    }

    public override async Task<bool> EvaluateAsync(
        TimeSlot slot,
        SchedulingContext context,
        CancellationToken ct = default)
    {
        return await EvaluateCoreAsync(slot, context, ct);
    }

    protected override async Task<bool> EvaluateCoreAsync(
        TimeSlot slot,
        SchedulingContext context,
        CancellationToken ct)
    {
        // Проверка основного ресурса
        var isMainAvailable = await _availabilityService.IsResourceAvailableAsync(
            _mainResourceId,
            slot.Start,
            slot.End,
            ct
        );

        if (!isMainAvailable) return false;

        // Проверка зависимых ресурсов
        foreach (var resourceId in _dependentResourceIds)
        {
            var isAvailable = await _availabilityService.IsResourceAvailableAsync(
                resourceId,
                slot.Start,
                slot.End,
                ct
            );

            if (!isAvailable) return false;
        }

        return true;
    }
}