using System;

namespace VC.Tenants.Api.Endpoints.Tenants.Models
{
    public record TenantWeekWorkScheduleDto(
        List<TenantDayWorkScheduleDto> WeekDays
        );

    public static class TenantWorkScheduleMapper
    {
        public static Application.Tenants.Models.TenantWeekWorkSheduleDto ToTenantWeekWorkSheduleDto(this Api.Endpoints.Tenants.Models.TenantWeekWorkScheduleDto dto)
        {
            return new Application.Tenants.Models.TenantWeekWorkSheduleDto(dto.WeekDays.ToTenantsDayWorkShedule());
        }
    }
}