namespace VC.Recources.Domain.Entities;

public class Resource
{
    public Guid TenantId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<Skill>? Skills { get; set; }

    private const int _highestNumberOfSkills = 20;

    public Resource() { }

    public Resource(string name, string description)
    {
        UpdateName(name);
        UpdateDescription(description);
    }

    public void UpdateDetails(
        string name,
        string description,
        List<Skill> skills)
    {
        UpdateName(name);
        UpdateDescription(description);
        UpdateSkills(skills);
    }

    private void UpdateSkills(IEnumerable<Skill> newSkills)
    {
        Skills.Clear();
        foreach (var skill in newSkills)
        {
            AddSkill(skill);
        }
    }

    private void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("Name cannot be empty");

        Name = name.Trim();
    }

    private void UpdateDescription(string description)
    {
        Description = description.Trim();
    }

    private void AddSkill(Skill skill)
    {
        if (Skills.Count >= _highestNumberOfSkills)
            throw new InvalidOperationException("Max skills limit reached");

        Skills.Add(skill);
    }
}