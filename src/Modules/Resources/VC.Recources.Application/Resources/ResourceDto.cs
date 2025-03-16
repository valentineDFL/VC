using System.Security.AccessControl;

namespace VC.Recources.Application.Resources;

public  class ResourceDto
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public ResourceType ResourceType { get; set; }

    public double Rating { get; set; }

    public string Description { get; set; } 

    public bool IsActive { get; set; }
}


