namespace VC.Scheduling.Ranges;

public sealed class DateTimeRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public DateTimeRange(DateTime start, DateTime end)
    {
        if (start >= end)
            throw new DomainException("Конец диапазона должен быть позже начала");
        
        Start = start;
        End = end;
    }

    public bool Overlaps(DateTimeRange other)
        => Start < other.End && End > other.Start;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}