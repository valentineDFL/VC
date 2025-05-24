namespace VC.Core.Api.Models.Resources.CreateResource;

public record CreateResourceRequest(
    string Title,
    string Description,
    int Count);
