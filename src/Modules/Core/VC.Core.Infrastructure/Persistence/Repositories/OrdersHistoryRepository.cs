using Microsoft.EntityFrameworkCore;
using VC.Core.Repositories;
using VC.Core.Services;

namespace VC.Core.Infrastructure.Persistence.Repositories;

internal class OrdersHistoryRepository : BaseRepository<OrderSnapshot, Guid>, IOrdersHistoryRepository
{
    public OrdersHistoryRepository(DatabaseContext dbContext) : base(dbContext) 
    {
    }

    public async Task<List<OrderSnapshot>> GetAllByEmployeeIdAndDateAsync(Guid employeeId, DateOnly date)
        => await DbSet.Where(oh => oh.EmployeesIds.Contains(employeeId) &&
                             DateOnly.FromDateTime(oh.ServiceTime) == date).ToListAsync();

    public async Task<OrderSnapshot> GetByOrderIdAsync(Guid orderId)
        => await DbSet.FirstOrDefaultAsync(os => os.OrderId == orderId);

}