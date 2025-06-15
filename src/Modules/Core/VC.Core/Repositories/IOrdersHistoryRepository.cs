using VC.Core.Services;

namespace VC.Core.Repositories;

public interface IOrdersHistoryRepository : IRepository<OrderSnapshot, Guid>
{
    public Task<List<OrderSnapshot>> GetAllByEmployeeIdAndDateAsync(Guid employeeId, DateOnly date);

    public Task<OrderSnapshot> GetByOrderIdAsync(Guid orderId);
}