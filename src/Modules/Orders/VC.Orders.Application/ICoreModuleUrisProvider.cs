namespace VC.Orders.Application;

public interface ICoreModuleUrisProvider
{
    public string GetServiceById(Guid id);

    public string GetEmployeeById(Guid id);
}