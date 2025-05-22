namespace VC.Orders.Application.Dtos.Get;

public class ResourceDto
{
    public ResourceDto(Guid id, string title, int count)
    {
        Id = id; 
        Title = title; 
        Count = count;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public int Count { get; private set; }
}