namespace VC.Scheduling.AvailabilityRules;

/// <summary>
/// Базовый класс для всех типов правил доступности.
/// </summary>
public abstract class AvailabilityRule
{
    public abstract bool IsSlotAvailable(TimeSlot slot, SchedulingContext context);
}