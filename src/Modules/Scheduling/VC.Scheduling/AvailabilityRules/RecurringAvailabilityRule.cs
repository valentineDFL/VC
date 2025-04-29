using System.Globalization;

namespace VC.Scheduling.AvailabilityRules;

/// <summary>
/// Проверка доступности по повторяющемуся расписанию.
/// </summary>
/// <example>
/// <code>
/// // 1. Создаем расписание
/// var workSchedule = new RecurringSchedule(
///     type: RecurrenceType.Weekly,
///     time: new TimeRange(TimeSpan.FromHours(9), TimeSpan.FromHours(18)),
///     activePeriod: new DateRange(DateTime.Today, DateTime.Today.AddYears(1)),
///     daysOfWeek: new[] { DayOfWeek.Monday, DayOfWeek.Wednesday }
/// );
/// 
/// // 2. Создаем правило на основе расписания
/// var availabilityRule = new RecurringAvailabilityRule(workSchedule);
/// 
/// // 3. Проверяем слот
/// var slot = new TimeSlot(
///     start: new DateTime(2024, 3, 25, 10, 0, 0), // Понедельник, 10:00
///     end: new DateTime(2024, 3, 25, 11, 0, 0)
/// );
/// 
/// bool isAvailable = availabilityRule.IsSlotAvailable(slot, context); // → true
/// </code>
/// </example>
public sealed class RecurringAvailabilityRule : AvailabilityRule
{
    private readonly RecurringSchedule _schedule;

    public RecurringAvailabilityRule(RecurringSchedule schedule)
    {
        _schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
    }

    public override bool IsSlotAvailable(TimeSlot slot, SchedulingContext context)
    {
        // 1. Проверка попадания слота в активный период расписания
        if (!_schedule.ActivePeriod.Includes(slot.Start.Date))
            return false;

        // 2. Проверка соответствия дню недели (для Weekly)
        if (_schedule.Type == RecurrenceType.Weekly &&
            !_schedule.DaysOfWeek.Contains(slot.Start.DayOfWeek))
            return false;

        // 3. Проверка временного диапазона
        var slotTimeStart = slot.Start.TimeOfDay;
        var slotTimeEnd = slot.End.TimeOfDay;
        
        if (slotTimeStart < _schedule.Time.Start || 
            slotTimeEnd > _schedule.Time.End)
            return false;

        // 4. Проверка интервала
        return IsIntervalValid(slot.Start.Date);
    }

    private bool IsIntervalValid(DateTime date)
    {
        return _schedule.Type switch
        {
            RecurrenceType.Daily => 
                (date - _schedule.ActivePeriod.StartDate.Date).Days % _schedule.Interval == 0,

            RecurrenceType.Weekly => 
                (GetWeekNumber(date) - GetWeekNumber(_schedule.ActivePeriod.StartDate)) 
                % _schedule.Interval == 0,

            RecurrenceType.Monthly => 
                (date.Month - _schedule.ActivePeriod.StartDate.Month) 
                % _schedule.Interval == 0,

            RecurrenceType.Yearly => 
                date.Year % _schedule.Interval == 0,

            _ => throw new NotSupportedException()
        };
    }

    private int GetWeekNumber(DateTime date)
        => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            date, 
            CalendarWeekRule.FirstFourDayWeek, 
            DayOfWeek.Monday
        );
}