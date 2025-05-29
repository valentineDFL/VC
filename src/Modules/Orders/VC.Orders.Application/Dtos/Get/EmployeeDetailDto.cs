namespace VC.Orders.Application.Dtos.Get;

public class EmployeeDetailDto
{
    public EmployeeDetailDto(Guid id, string fullName, 
                             string specialisation, decimal price)
    {
        Id = id;
        FullName = fullName;
        Specialisation = specialisation;
        Price = price;
    }

    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    public string Specialisation { get; private set; }

    public decimal Price { get; private set; }
}