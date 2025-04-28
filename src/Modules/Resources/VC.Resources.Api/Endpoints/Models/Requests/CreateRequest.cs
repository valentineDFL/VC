using VC.Recources.Application.Endpoints.Models.Dto;
using VC.Recources.Application.Endpoints.Models.Requests;

namespace VC.Resources.Api.Endpoints.Models.Requests;

public record CreateRequest(
    string Name,
    string Description,
    List<SkillDto> Skills
    );

public static class CreateMappers
{
    public static CreateDto ToCreateDto(this CreateRequest dto)
        => new CreateDto(dto.Name, dto.Description, dto.Skills.ToList());
}
