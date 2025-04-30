

namespace VC.Scheduling;

/// <summary>
/// Переопределение расписания.
/// </summary>
/// <remarks>
/// Используется для временных исключений из основного расписания ресурса.
/// Он позволяет гибко управлять доступностью ресурса в конкретные даты, не изменяя базовых правил расписания. <br/>
///
/// Временные исключения: <br/>
/// - Отметить выходной день (например, мастер ушел в отпуск 10 марта).<br/>
/// - Добавить внеурочную работу (например, продлить часы работы в праздничный день).<br/>
/// - Технические перерывы (например, оборудование недоступно 15 марта с 14:00 до 16:00).<br/>
/// Примеры использования:<br/>
/// - Обычное расписание: мастер работает по понедельникам с 9:00 до 18:00.<br/>
/// - Переопределение: 25 марта мастер не работает (выходной).<br/>
/// </remarks>
/// <example>
/// <code>
/// // Переопределение: недоступен 25 марта с 14:00 до 16:00
/// var overridePeriod = new DateTimeRange(
///     new DateTime(2024, 3, 25, 14, 0, 0),
///     new DateTime(2024, 3, 25, 16, 0, 0)
/// );
/// 
/// var scheduleOverride = new ScheduleOverride(
///     overridePeriod, 
///     isAvailable: false, 
///     reason: "Технический перерыв"
/// );
/// 
/// // Проверка влияния на слот 25 марта 15:00-15:30
/// var slot = new TimeSlot(
///     new DateTime(2024, 3, 25, 15, 0, 0),
///     new DateTime(2024, 3, 25, 15, 30, 0)
/// );
/// 
/// bool isAffected = scheduleOverride.Affects(slot); // → true
/// </code>
/// </example>
public sealed class ScheduleOverride : ValueObject
{
    public DateTimeRange Period { get; }
    
    /// <summary>
    /// Доступен ли ресурс в этот период.
    /// </summary>
    public bool IsAvailable { get; }
    
    /// <summary>
    /// Причина переопределения (например, "отпуск").
    /// </summary>
    public string Reason { get; }

    public ScheduleOverride(DateTimeRange period, bool isAvailable, string reason)
    {
        Period = period ?? throw new ArgumentNullException(nameof(period));
        IsAvailable = isAvailable;
        Reason = reason;
    }

    public bool Affects(TimeSlot slot)
        => Period.Overlaps(new DateTimeRange(slot.Start, slot.End));

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Period;
        yield return IsAvailable;
        yield return Reason;
    }
}