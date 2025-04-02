using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Application.Endpoints.Models.Requests;

namespace VC.Resources.Api.Endpoints.Models.Requests;

public record CreateResourceRequest(
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class CreateResourceMappers
{
    public static CreateResourceDto ToCreateResourceDto(this CreateResourceRequest dto)
        => new CreateResourceDto(dto.Name, dto.Description, dto.Skills.ToList());
}
