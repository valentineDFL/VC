using VC.Core.Employees;

namespace VC.Core.Repositories;

public interface IEmployeesRepository : IRepository<Employee, Guid>
{
}