using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC.Tenants.Models
{
    public class TenantDayWorkSchedule
    {
        public DayOfWeek Day { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
    }
}