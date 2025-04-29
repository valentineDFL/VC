namespace VC.Services.Api.Models.Resources.Responses;

public class Response(
    string name,
    string description)
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;
};

internal static class ResponseMapper
{
    public static Response ToResponseDto(this Resource dto)
        => new Response(dto.Title, dto.Description);
}