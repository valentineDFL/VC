using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models.Request;

public record UpdateTenantRequest(
    Guid TenantId,
    string Name,
    string Slug,
    TenantConfigurationDto Config,
    TenantStatus Status,
    ContactInfoDto Contact,
    TenantWeekWorkScheduleDto WorkSchedule
    )
{
    public Guid TenantId { get; init; } = TenantId;

    public string Name { get; init; } = Name;

    public string Slug { get; init; } = Slug;

    public TenantConfigurationDto Config { get; init; } = Config;

    public TenantStatus Status { get; init; } = Status;

    public ContactInfoDto Contact { get; init; } = Contact;

    public TenantWeekWorkScheduleDto WorkSchedule { get; init; } = WorkSchedule;
}

public static class UpdateTenantRequestMappers 
{
    public static Application.Tenants.Models.UpdateTenantParams ToTenantUpdateDto(this UpdateTenantRequest dto)
    {
        return new Application.Tenants.Models.UpdateTenantParams(dto.TenantId,
            dto.Name,
            dto.Slug,
            dto.Config.ToTenantConfigurationDto(),
            dto.Status,
            dto.Contact.ToApplicationContactDto(),
            dto.WorkSchedule.ToTenantWeekWorkSheduleDto());
    }
}
