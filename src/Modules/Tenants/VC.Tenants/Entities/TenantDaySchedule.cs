namespace VC.Tenants.Entities;

public class TenantDaySchedule
{
    private TenantDaySchedule(Guid id, DayOfWeek day, DateTime startWork, DateTime endWork)
    {
        Id = id;
        Day = day;
        StartWork = startWork;
        EndWork = endWork;
    }

    private TenantDaySchedule() { }

    public Guid Id { get; private set; }

    public DayOfWeek Day { get; private set; }

    public DateTime StartWork { get; private set; }

    public DateTime EndWork { get; private set; }

    public static TenantDaySchedule Create(Guid id, DayOfWeek day, DateTime startWork, DateTime endWork)
    {
        if(id == Guid.Empty)
            throw new ArgumentNullException("Id cannot be empty");

        if (startWork > endWork)
            throw new ArgumentException("StartWork time cannot be highest than EndWork time");

        if(startWork == endWork)
            throw new ArgumentException("StartWork time cannot be equals EndWork time");

        return new TenantDaySchedule(id, day, startWork, endWork);
    }
}