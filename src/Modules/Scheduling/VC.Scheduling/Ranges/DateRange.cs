namespace VC.Scheduling.Ranges;

public sealed class DateRange : ValueObject
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public DateRange(DateTime start, DateTime end)
    {
        if (start > end)
            throw new DomainException("Конец диапазона должен быть позже начала");
        
        StartDate = start;
        EndDate = end;
    }

    public bool Includes(DateTime date)
        => date >= StartDate && date <= EndDate;

    public bool Overlaps(DateRange other)
        => StartDate < other.EndDate && EndDate > other.StartDate;
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}