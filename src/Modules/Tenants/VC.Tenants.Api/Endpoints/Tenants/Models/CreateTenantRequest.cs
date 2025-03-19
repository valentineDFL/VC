using System.ComponentModel;
using VC.Tenants.Application.Tenants.Models;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Endpoints.Tenants.Models;

public record CreateTenantRequest(
    string Name,
    string Slug,
    TenantConfigurationDto Config,
    TenantStatus Status,
    ContactInfoDto Contact,
    TenantWeekWorkScheduleDto WorkSchedule
    );

public static class CreateTenantRequestMappers
{
    public static CreateTenantParams ToCreateTenantParams(this CreateTenantRequest request)
    {
        return new CreateTenantParams(request.Name, request.Slug, request.Config.ToTenantConfigurationDto(),
            request.Status, request.Contact.ToContactDto(), request.WorkSchedule.ToTenantWeekWorkSheduleDto());
    }
}