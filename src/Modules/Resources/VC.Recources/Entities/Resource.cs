namespace VC.Recources.Domain.Entities;

public class Resource
{
    public Guid TenantId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<Skill>? Skills { get; set; }

    public Resource()
    {
    }

    public async Task UpdateDetails(
        string name,
        INameUniquenessChecker nameUniquenessChecker,
        string description,
        List<Skill> skills)
    {
        await UpdateName(name, nameUniquenessChecker);
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

    private async Task UpdateName(string newName, INameUniquenessChecker nameUniquenessChecker)
    {
        if (!await nameUniquenessChecker.IsNameUniqueAsync(newName, Id))
            throw new InvalidOperationException($"Name {newName} already exists");

        if (string.IsNullOrWhiteSpace(newName))
            throw new InvalidOperationException("Name cannot be empty");

        Name = newName.Trim();
    }

    private void UpdateDescription(string description)
    {
        Description = description.Trim();
    }

    private void AddSkill(Skill skill)
    {
        if (skill is not null)
            Skills.Add(skill);
    }
}