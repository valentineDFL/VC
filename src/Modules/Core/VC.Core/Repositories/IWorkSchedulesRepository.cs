using VC.Core.Employees;

namespace VC.Core.Repositories;

public interface IWorkSchedulesRepository : IRepository<WorkSchedule, Guid>
{
    public Task<WorkSchedule?> GetByEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default);
    
    Task<WorkSchedule?> GetByEmployeeAndDateAsync(
        Guid employeeId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}