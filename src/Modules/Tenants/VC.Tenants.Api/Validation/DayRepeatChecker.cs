using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Api.Endpoints.Tenants.Models;

namespace VC.Tenants.Api.Validation
{
    internal static class DayRepeatChecker
    {
        public static bool IsDayRepeat(List<TenantDayWorkScheduleDto> workDays)
        {
            for (int i = 0; i < workDays.Count; i++)
            {
                for (int j = 0; j < workDays.Count; j++)
                {
                    if (j == i)
                        continue;

                    if (workDays[i].Day == workDays[j].Day)
                        return true;
                }
            }

            return false;
        }
    }
}
