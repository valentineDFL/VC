using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Application.ServicesUseCases.Mapping;

public static class MapperToDto
{
    public static ServiceDto ConvertToDto(Service service)
    {
        var serviceDto = new ServiceDto()
        {
            Id = service.Id,
            Title = service.Title,
            Description = service.Description,
            Price = service.BasePrice,
            Duration = service.BaseDuration,
            IsActive = service.IsActive,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt,
            RequiredResources = service.RequiredResources,
        };

        return serviceDto;
    }

    public static List<ServiceDto> ConvertToDtoList(ICollection<Service> services) => services.Select(ConvertToDto).ToList();
}
