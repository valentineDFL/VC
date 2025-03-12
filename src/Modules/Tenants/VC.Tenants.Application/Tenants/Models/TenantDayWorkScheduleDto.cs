namespace VC.Tenants.Application.Tenants.Models;

public class TenantDayWorkScheduleDto(DayOfWeek day, DateTime startWork, DateTime endWork)
{
    public DayOfWeek Day { get; } = day;

    public DateTime StartWork { get; } = startWork;

    public DateTime EndWork { get; } = endWork;
}