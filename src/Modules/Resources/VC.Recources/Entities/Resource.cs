namespace VC.Recources.Domain.Entities;

public class Resource
{
    public Guid TenantId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<Skill>? Skills { get; set; }
}
