namespace VC.Scheduling;

/// <summary>
/// Представление временного интервала в системе бронирования
/// </summary>
public sealed class TimeSlot : ValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }
    public SlotStatus Status { get; }

    public TimeSlot(DateTime start, DateTime end, SlotStatus status = SlotStatus.Available)
    {
        if (start >= end)
            throw new DomainException("Invalid time slot");
        
        Start = start;
        End = end;
        Status = status;
    }

    public TimeSpan Duration => End - Start;

    /// <summary>
    /// Проверяет пересечение с другим слотом
    /// </summary>
    public bool Overlaps(TimeSlot other) => Start < other.End && End > other.Start;

    /// <summary>
    /// Проверяет полное вхождение другого слота
    /// </summary>
    public bool Contains(TimeSlot other) => Start <= other.Start && End >= other.End;
    
    /// <summary>
    /// Создает копию слота с новым статусом
    /// </summary>
    public TimeSlot WithStatus(SlotStatus newStatus)
        => new TimeSlot(Start, End, newStatus);
    
    /// <summary>
    /// Применяет переопределение расписания
    /// </summary>
    public TimeSlot ApplyOverride(ScheduleOverride @override)
    {
        if (!@override.Period.Overlaps(new DateTimeRange(Start, End)))
            return this;

        return @override.IsAvailable 
            ? WithStatus(SlotStatus.Available) 
            : WithStatus(SlotStatus.Blocked);
    }

    /// <summary>
    /// Разбивает слот на части относительно переопределения
    /// </summary>
    public IEnumerable<TimeSlot> Split(DateTimeRange splitRange)
    {
        var parts = new List<TimeSlot>();

        // Часть до переопределения
        if (Start < splitRange.Start)
            parts.Add(new TimeSlot(Start, splitRange.Start, SlotStatus.Split));

        // Часть после переопределения
        if (End > splitRange.End)
            parts.Add(new TimeSlot(splitRange.End, End, SlotStatus.Split));

        return parts.Count != 0 ? parts : new[] { this.WithStatus(SlotStatus.Split) };
    }
    
    public override string ToString()
        => $"{Start:yyyy-MM-dd HH:mm} - {End:HH:mm} ({Status})";
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
        yield return Status;
    }
}