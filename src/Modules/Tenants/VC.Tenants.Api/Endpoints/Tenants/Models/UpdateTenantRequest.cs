using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

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
