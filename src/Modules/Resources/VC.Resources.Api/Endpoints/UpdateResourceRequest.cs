namespace VC.Resources.Api.Endpoints;

public record UpdateResourceRequest(
    string Name, 
    string Description,
    Dictionary<string, object> Attributes
    );
