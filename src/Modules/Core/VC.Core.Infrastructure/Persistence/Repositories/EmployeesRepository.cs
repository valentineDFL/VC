using VC.Core.Employees;
using VC.Core.Repositories;

namespace VC.Core.Infrastructure.Persistence.Repositories;

internal class EmployeesRepository : BaseRepository<Employee, Guid>, IEmployeesRepository
{
    public EmployeesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}