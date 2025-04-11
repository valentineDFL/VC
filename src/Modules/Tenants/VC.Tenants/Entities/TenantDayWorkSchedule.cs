namespace VC.Tenants.Entities;

public class TenantDayWorkSchedule
{
    private TenantDayWorkSchedule(DayOfWeek day, DateTime startWork, DateTime endWork)
    {
        Day = day;
        StartWork = startWork;
        EndWork = endWork;
    }

    public DayOfWeek Day { get; private set; }

    public DateTime StartWork { get; private set; }

    public DateTime EndWork { get; private set; }

    public static TenantDayWorkSchedule Create(DayOfWeek day, DateTime startWork, DateTime endWork)
    {
        if (startWork > endWork)
            throw new ArgumentException("StartWork time cannot be highest than EndWork time");

        if(startWork == endWork)
            throw new ArgumentException("StartWork time cannot be equals EndWork time");

        return new TenantDayWorkSchedule(day, startWork, endWork);
    }
}