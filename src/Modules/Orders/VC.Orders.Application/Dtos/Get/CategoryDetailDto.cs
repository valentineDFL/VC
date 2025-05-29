namespace VC.Orders.Application.Dtos.Get;

public class CategoryDetailDto
{
    public CategoryDetailDto(Guid id, string title)
    {
        Id = id;
        Title = title;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }
}