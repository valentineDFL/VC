namespace VC.Services.Api.Models.Resources.CreateResource;

public record CreateResourceRequest(
    string Title,
    string Description,
    int Count);
