using System.Security.AccessControl;

namespace VC.Resources.Api.Endpoints;

public record CreateResourceRequest(
    string Name,
    string Description,
    ResourceType ResourceType,
    Dictionary<string,object> Attributes
    );

