namespace VC.Tenants.Application.Tenants.Models;

public class TenantWeekWorkSheduleDto(IReadOnlyList<TenantDayWorkScheduleDto> workDays)
{
    public IReadOnlyList<TenantDayWorkScheduleDto> WorkDays { get; } = workDays;
}
