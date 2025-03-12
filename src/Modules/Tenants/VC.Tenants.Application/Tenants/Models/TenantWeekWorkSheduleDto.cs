using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC.Tenants.Application.Tenants.Models
{
    public class TenantWeekWorkSheduleDto(IReadOnlyList<TenantDayWorkScheduleDto> workDays)
    {
        public IReadOnlyList<TenantDayWorkScheduleDto> WorkDays { get; } = workDays;
    }
}
