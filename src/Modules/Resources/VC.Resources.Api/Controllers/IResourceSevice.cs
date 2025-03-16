using VC.Recources.Resource.Domain.Entities;

namespace VC.Resources.Api.Controllers;

public interface IResourceSevice
{
    public Task<Resource> CreateResourceAsync(
        Guid tenantId,
        string name,
        string description,
        ResourceType resourceType,
        Dictionary<string, object> attributes
        );

    public Task<Resource> GetResourceAsync(Guid tenantId, Guid resourceId);

    public Task<bool> UpdateResourceAsync(
        Guid tenantId,
        string name,
        string description,
        Dictionary<string, object> attributes
        );
}
