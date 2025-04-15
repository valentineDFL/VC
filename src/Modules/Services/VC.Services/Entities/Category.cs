namespace VC.Services.Entities;
public class Category
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public Category? ParentCategory { get; set; }
}
