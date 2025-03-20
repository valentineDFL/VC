using System.ComponentModel;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models.Request;

public record CreateTenantRequest(
    string Name,
    string Slug,
    TenantConfigurationDto Config,
    TenantStatus Status,
    ContactInfoDto Contact,
    TenantWeekWorkScheduleDto WorkSchedule
    )
{
    public string Name { get; init; } = Name;

    public string Slug { get; init; } = Slug;

    public TenantConfigurationDto Config { get; init; } = Config;

    public TenantStatus Status { get; init; } = Status;

    public ContactInfoDto Contact { get; init; } = Contact;

    public TenantWeekWorkScheduleDto WorkSchedule { get; init; } = WorkSchedule;
}

public static class CreateTenantRequestMappers
{
    public static CreateTenantParams ToCreateTenantParams(this CreateTenantRequest request)
    {
        return new CreateTenantParams(request.Name, request.Slug, request.Config.ToTenantConfigurationDto(),
            request.Status, request.Contact.ToApplicationContactDto(), request.WorkSchedule.ToTenantWeekWorkSheduleDto());
    }
}