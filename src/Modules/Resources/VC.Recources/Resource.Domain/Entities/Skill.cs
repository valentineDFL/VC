namespace VC.Recources.Resource.Domain.Entities;

public class Skill
{
    public Guid Id { get; set; }

    public string SkillName { get; set; }

    public Experience Expirience { get; set; }

    public Resource Resource { get; set; }

    public Guid ResourceId { get; set; }
}
