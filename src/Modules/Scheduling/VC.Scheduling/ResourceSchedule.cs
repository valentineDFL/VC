namespace VC.Scheduling;

/// <summary>
/// Агрегат для управления расписанием ресурса
/// </summary>
public class ResourceSchedule : AggregateRoot<Guid>
{
    // Идентификатор ресурса из модуля Resources
    public Guid ResourceId { get; }

    // Основные правила расписания
    private readonly List<RecurringSchedule> _recurringSchedules = new();
    public IReadOnlyList<RecurringSchedule> RecurringSchedules => _recurringSchedules.AsReadOnly();

    // Временные переопределения
    private readonly List<ScheduleOverride> _overrides = new();
    public IReadOnlyList<ScheduleOverride> Overrides => _overrides.AsReadOnly();

    // Приватный конструктор для EF Core
    private ResourceSchedule() { }

    public ResourceSchedule(Guid resourceId)
    {
        if (resourceId == Guid.Empty)
            throw new DomainException("ResourceId cannot be empty", "INVALID_RESOURCE_ID");
            
        ResourceId = resourceId;
        Id = Guid.NewGuid(); // Или использовать ResourceId как Id агрегата
    }

    // Добавление повторяющегося расписания
    public void AddRecurringSchedule(RecurringSchedule schedule)
    {
        // Проверка на пересечение с существующими расписаниями
        if (_recurringSchedules.Any(s => s.OverlapsWith(schedule)))
            throw new DomainException("Schedule overlaps with existing", "SCHEDULE_CONFLICT");
            
        _recurringSchedules.Add(schedule);
    }

    // Добавление временного переопределения
    public void AddOverride(ScheduleOverride @override)
    {
        // Проверка на пересечение с существующими переопределениями
        if (_overrides.Any(o => o.Period.Overlaps(@override.Period)))
            throw new DomainException("Override overlaps with existing", "OVERRIDE_CONFLICT");
            
        _overrides.Add(@override);
    }

    /// <summary>
    /// Генерация доступных слотов с учетом всех правил и переопределений
    /// </summary>
    public IEnumerable<TimeSlot> GenerateSlots(DateRange period)
    {
        // 1. Генерация базовых слотов по повторяющимся расписаниям
        var baseSlots = _recurringSchedules
            .SelectMany(s => s.GenerateSlots(period))
            .OrderBy(s => s.Start)
            .ToList();

        // 2. Применение переопределений
        foreach (var slot in baseSlots)
        {
            var overrideRule = _overrides.FirstOrDefault(o => o.Affects(slot));
            yield return overrideRule != null 
                ? ApplyOverride(slot, overrideRule) 
                : slot;
        }
    }

    private TimeSlot ApplyOverride(TimeSlot slot, ScheduleOverride @override)
    {
        // Логика применения переопределения:
        // - Если переопределение блокирует слот, помечаем его как недоступный
        // - Если разрешает, проверяем пересечение с оригинальным расписанием
            
        return @override.IsAvailable 
            ? HandlePartialOverride(slot, @override.Period) 
            : slot.WithStatus(SlotStatus.Blocked);
    }

    private TimeSlot HandlePartialOverride(TimeSlot slot, DateTimeRange overridePeriod)
    {
        // Создаем уточненные слоты при частичном переопределении
        var slots = new List<TimeSlot>();

        // Слот до переопределения
        if (slot.Start < overridePeriod.Start)
            slots.Add(new TimeSlot(slot.Start, overridePeriod.Start));

        // Слот после переопределения
        if (slot.End > overridePeriod.End)
            slots.Add(new TimeSlot(overridePeriod.End, slot.End));

        return slots.Count != 0
            ? new TimeSlot(slot.Start, slot.End, SlotStatus.Split) 
            : slot.WithStatus(SlotStatus.Available);
    }
}