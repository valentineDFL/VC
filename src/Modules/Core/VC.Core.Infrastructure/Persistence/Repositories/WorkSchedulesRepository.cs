using Microsoft.EntityFrameworkCore;
using VC.Core.Employees;
using VC.Core.Repositories;

namespace VC.Core.Infrastructure.Persistence.Repositories;

internal class WorkSchedulesRepository : BaseRepository<WorkSchedule, Guid>, IWorkSchedulesRepository
{
    public WorkSchedulesRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public Task<WorkSchedule?> GetByEmployeeAsync(Guid employeeId, CancellationToken cancellationToken = default) 
        => DbSet.FirstOrDefaultAsync(w => w.EmployeeId == employeeId, cancellationToken);

    public Task<WorkSchedule?> GetByEmployeeAndDateAsync(Guid employeeId, DateOnly date, CancellationToken cancellationToken = default)
    {
        var dayOfWeek = date.ToDateTime(TimeOnly.MinValue).DayOfWeek;

         var queryDayOfWeek = $$"""
                       [{"DayOfWeek": {{(int)dayOfWeek}}}]
                       """;

        return DbSet.FirstOrDefaultAsync(w => w.EmployeeId == employeeId &&
                                              EF.Functions.JsonContains(w.Items, queryDayOfWeek), cancellationToken);
    }
}