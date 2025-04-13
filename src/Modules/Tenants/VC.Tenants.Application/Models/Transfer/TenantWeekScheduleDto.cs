namespace VC.Tenants.Application.Models.Transfer;

public class TenantWeekScheduleDto(IReadOnlyList<TenantDayScheduleDto> workDays)
{
    public IReadOnlyList<TenantDayScheduleDto> WorkDays { get; } = workDays;
}