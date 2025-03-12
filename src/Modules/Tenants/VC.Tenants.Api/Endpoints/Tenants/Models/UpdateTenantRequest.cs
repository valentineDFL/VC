using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Models;

namespace VC.Tenants.Api.Endpoints.Tenants.Models
{
    public record UpdateTenantRequest(
        Guid TenantId,
        string Name,
        string Slug,
        TenantConfigurationDto Config,
        TenantStatus Status,
        ContactInfoDto Contact,
        TenantWeekWorkScheduleDto WorkSchedule
        );

    public static class UpdateTenantRequestMappers 
    {
        public static Application.Tenants.Models.UpdateTenantParams ToTenantUpdateDto(this UpdateTenantRequest dto)
        {
            return new Application.Tenants.Models.UpdateTenantParams(dto.TenantId,
                dto.Name,
                dto.Slug,
                dto.Config.ToTenantConfigurationDto(),
                dto.Status,
                dto.Contact.ToContactDto(),
                dto.WorkSchedule.ToTenantWeekWorkSheduleDto());
        }
    }
}
