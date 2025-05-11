namespace VC.Core.Api.Models.Resources.UpdateResource;

public record UpdateResourceRequest(
    string Title,
    string Description,
    int Count);