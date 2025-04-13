using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Transfer;

public class TenantDayScheduleDto(Guid id, DayOfWeek day, DateTime startWork, DateTime endWork)
{
    public Guid Id { get; } = id;

    public DayOfWeek Day { get; } = day;

    public DateTime StartWork { get; } = startWork;

    public DateTime EndWork { get; } = endWork;
}