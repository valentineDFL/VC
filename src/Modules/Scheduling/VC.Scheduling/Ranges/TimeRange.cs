namespace VC.Scheduling.Ranges;

public sealed class TimeRange : ValueObject
{
    public TimeSpan Start { get; }
    public TimeSpan End { get; }

    public TimeRange(TimeSpan start, TimeSpan end)
    {
        if (start >= end)
            throw new DomainException("Конец диапазона должен быть позже начала");
        
        Start = start;
        End = end;
    }

    public bool Overlaps(TimeRange other)
        => Start < other.End && End > other.Start;
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}