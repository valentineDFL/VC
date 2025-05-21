namespace VC.Orders.Application.Dtos.Get;

public class ServiceDetailDto
{
    public ServiceDetailDto(Guid id, 
                            string title, 
                            string description, 
                            decimal basePrice, 
                            TimeSpan baseDuration, 
                            CategoryDetailDto? category, 
                            List<ResourceDto> resources, 
                            List<EmployeeDetailDto> employees)
    {
        Id = id;
        Title = title;
        Description = description;
        BasePrice = basePrice;
        BaseDuration = baseDuration;
        Category = category;
        Resources = resources;
        Employees = employees;
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public decimal BasePrice { get; private set; }

    public TimeSpan BaseDuration { get; private set; }

    public CategoryDetailDto? Category { get; private set; }

    public IReadOnlyList<ResourceDto> Resources { get; private set; }

    public IReadOnlyList<EmployeeDetailDto> Employees { get; private set; }
}