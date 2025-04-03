namespace VC.Recources.Domain.Entities;

public class Skill
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Experience Experience { get; set; }

    public Resource Resource { get; set; }

    public Guid ResourceId { get; set; }

    public Skill(string name, Experience experience)
    {
        Name = name;
        Experience = experience;
    }

    public Skill() { }
}