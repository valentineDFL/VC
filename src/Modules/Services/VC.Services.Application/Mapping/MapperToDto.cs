using VC.Services.Application.Models;
using VC.Services.Entities;

namespace VC.Services.Application.Mapping;

public static class MapperToDto
{
    public static ServiceDto ConvertToDto(Service service)
    {
        ServiceDto serviceDto = new(
            service.Id,
            service.Title,
            service.Description,
            service.Price,
            service.Duration,
            service.Category,
            service.IsActive,
            service.CreatedAt,
            service.UpdatedAt,
            service.ResourceRequirement);

        return serviceDto;
    }

    public static List<ServiceDto> ConvertToDtoList(List<Service> services)
    {
        return services.Select(ConvertToDto).ToList();
    }
}
