namespace VC.Services.Entities;
public class Category
{
    private Category(Guid id, string title)
    {
        Id = id;
        Title = title;
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public Category? ParentCategory { get; set; }

    public Category Create(Guid id, string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("Title");

        return new Category(Id, title);
    }
}
