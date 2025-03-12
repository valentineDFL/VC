using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Models;

namespace VC.Tenants.Application.Tenants.Models
{
    public class UpdateTenantParams(Guid tenantId, string name, string slug,
        TenantConfigurationDto tenantConfig, TenantStatus tenantStatus,
        ContactDto contactDto, TenantWeekWorkSheduleDto worScheduleDto)
    {
        public Guid Id { get; } = tenantId;

        public string Name { get; } = name;

        public string Slug { get; } = slug;

        public TenantConfigurationDto TenantConfig { get; } = tenantConfig;

        public TenantStatus TenantStatus { get; } = tenantStatus;

        public ContactDto Contact { get; } = contactDto;

        public TenantWeekWorkSheduleDto WorkSchedule { get; } = worScheduleDto;
    }
}
