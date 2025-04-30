namespace VC.Scheduling;

/// <summary>
/// Повторяющееся расписание. Хранит данные о периодической доступности ресурса (например, рабочие часы мастера, график работы помещения). <br/>
/// Это ключевой компонент для моделирования регулярной доступности ресурсов в вашей системе.
/// </summary>
/// <remarks>
/// Для чего нужен RecurringSchedule? <br/>
/// Управление повторяющейся доступностью: <br/>
/// - Рабочие часы мастера: "Каждый понедельник и среду с 9:00 до 18:00" <br/>
/// - Технические перерывы: "Каждый день с 13:00 до 14:00" <br/>
/// - Сезонное расписание: "Каждую субботу июня с 10:00 до 20:00" <br/>
/// Генерация слотов: <br/>
/// - На основе параметров RecurringSchedule система автоматически создает временные слоты (TimeSlot) для бронирования. <br/>
/// </remarks>
/// <example>
/// <code>
/// // Создаем расписание
/// var schedule = new RecurringSchedule(
///     type: RecurrenceType.Weekly,
///     time: new TimeRange(TimeSpan.FromHours(9), TimeSpan.FromHours(18)),
///     activePeriod: new DateRange(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31)),
///     daysOfWeek: new[] { DayOfWeek.Monday, DayOfWeek.Wednesday }
/// );
/// 
/// // Генерируем слоты на март 2024
/// var march2024 = new DateRange(
///     new DateTime(2024, 3, 1),
///     new DateTime(2024, 3, 31)
/// );
/// 
/// var slots = schedule.GenerateSlots(march2024).ToList();
/// 
/// /* Результат:
///    04.03.2024 09:00-18:00
///    06.03.2024 09:00-18:00
///    11.03.2024 09:00-18:00
///    13.03.2024 09:00-18:00
///    ... и т.д.
/// */
/// </code>
/// </example>
public sealed class RecurringSchedule : ValueObject
{
    /// <summary>
    /// Тип повторения (ежедневно, еженедельно и т.д.).
    /// </summary>
    public RecurrenceType Type { get; }
    
    /// <summary>
    /// Временной диапазон в течение дня (например, 09:00-18:00).
    /// </summary>
    public TimeRange Time { get; }
    
    /// <summary>
    /// Период действия (например, 2024-01-01 — 2024-12-31).
    /// </summary>
    public DateRange ActivePeriod { get; }
    
    /// <summary>
    /// Интервал повторения (например, каждые 2 недели).
    /// </summary>
    public int Interval { get; }
    
    /// <summary>
    /// Дни недели (для еженедельного типа).
    /// </summary>
    public IReadOnlyList<DayOfWeek> DaysOfWeek { get; }

    public RecurringSchedule(
        RecurrenceType type,
        TimeRange time,
        DateRange activePeriod,
        int interval = 1,
        ICollection<DayOfWeek>? daysOfWeek = null)
    {
        // Валидация параметров
        if (interval < 1) throw new DomainException("Интервал должен быть ≥ 1", "INVALID_INTERVAL");
        if (type == RecurrenceType.Weekly && !daysOfWeek?.Any() == true)
            throw new DomainException("Для еженедельного типа нужны дни недели", "MISSING_DAYS");

        Type = type;
        Time = time;
        ActivePeriod = activePeriod;
        Interval = interval;
        DaysOfWeek = daysOfWeek?.ToList() ?? [];
    }

    /// <summary>
    /// Проверяет, пересекается ли текущее расписание с другим
    /// </summary>
    public bool OverlapsWith(RecurringSchedule other)
    {
        // 1. Проверка пересечения периодов активности
        if (!ActivePeriod.Overlaps(other.ActivePeriod))
            return false;

        // 2. Проверка временных диапазонов в течение дня
        if (!Time.Overlaps(other.Time))
            return false;

        // 3. Проверка логики повторений
        return Type switch
        {
            RecurrenceType.Daily => CheckDailyOverlap(other),
            RecurrenceType.Weekly => CheckWeeklyOverlap(other),
            RecurrenceType.Monthly => CheckMonthlyOverlap(other),
            RecurrenceType.Yearly => CheckYearlyOverlap(other),
            _ => throw new NotSupportedException()
        };
    }
    
    /// <summary>
    /// Генерирует слоты в заданном временном диапазоне
    /// </summary>
    public IEnumerable<TimeSlot> GenerateSlots(DateRange period)
    {
        var currentDate = period.StartDate.Date;
        while (currentDate <= period.EndDate.Date)
        {
            if (IsDateMatch(currentDate))
            {
                var start = currentDate.Add(Time.Start);
                var end = currentDate.Add(Time.End);
                yield return new TimeSlot(start, end);
            }
            currentDate = GetNextDate(currentDate);
        }
    }

    /// <summary>
    /// Проверяет, соответствует ли дата правилам расписания
    /// </summary>
    private bool IsDateMatch(DateTime date)
    {
        if (!ActivePeriod.Includes(date)) return false;

        return Type switch
        {
            RecurrenceType.Daily => (date - ActivePeriod.StartDate).Days % Interval == 0,
            RecurrenceType.Weekly => DaysOfWeek.Contains(date.DayOfWeek),
            RecurrenceType.Monthly => date.Day == ActivePeriod.StartDate.Day,
            RecurrenceType.Yearly => date.DayOfYear == ActivePeriod.StartDate.DayOfYear,
            _ => throw new NotSupportedException()
        };
    }

    /// <summary>
    /// Вычисляет следующую дату для генерации слотов
    /// </summary>
    private DateTime GetNextDate(DateTime current)
    {
        return Type switch
        {
            RecurrenceType.Daily => current.AddDays(Interval),
            RecurrenceType.Weekly => current.AddDays(7 * Interval),
            RecurrenceType.Monthly => current.AddMonths(Interval),
            RecurrenceType.Yearly => current.AddYears(Interval),
            _ => throw new NotSupportedException()
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return Time;
        yield return ActivePeriod;
        yield return Interval;
        
        // Для упорядоченной проверки дней недели
        foreach (var day in DaysOfWeek.OrderBy(d => d))
        {
            yield return day;
        }
    }
    
    private bool CheckDailyOverlap(RecurringSchedule other)
    {
        // Для ежедневных расписаний проверяем интервал
        return Interval == other.Interval 
               || (Interval % other.Interval == 0) 
               || (other.Interval % Interval == 0);
    }

    private bool CheckWeeklyOverlap(RecurringSchedule other)
    {
        // Общие дни недели + совместимые интервалы
        return DaysOfWeek.Intersect(other.DaysOfWeek).Any() 
               && Interval == other.Interval;
    }

    private bool CheckMonthlyOverlap(RecurringSchedule other)
    {
        // Одинаковый день месяца + совместимые интервалы
        return ActivePeriod.StartDate.Day == other.ActivePeriod.StartDate.Day 
               && Interval == other.Interval;
    }

    private bool CheckYearlyOverlap(RecurringSchedule other)
    {
        // Одинаковый день года + совместимые интервалы
        return ActivePeriod.StartDate.DayOfYear == other.ActivePeriod.StartDate.DayOfYear 
               && Interval == other.Interval;
    }
}