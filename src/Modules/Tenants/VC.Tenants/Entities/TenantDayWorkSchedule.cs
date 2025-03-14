namespace VC.Tenants.Entities;

public class TenantDayWorkSchedule
{
    public DayOfWeek Day { get; set; }
    public DateTime StartWork { get; set; }
    public DateTime EndWork { get; set; }
}