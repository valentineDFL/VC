using Microsoft.EntityFrameworkCore;
using VC.Orders.Payments;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class PaymentsStatusesRepository : IPaymentsStatusesRepository
{
    private readonly OrdersDbContext _dbContext;
    private readonly DbSet<PaymentStatus> _statuses;
    
    public PaymentsStatusesRepository(OrdersDbContext ordersDbContext)
    {
        _dbContext = ordersDbContext;
        _statuses = _dbContext.PaymentsStatuses;
    }

    public async Task<List<PaymentStatus>> GetByPaymentIdAsync(Guid paymentId)
    {
        return await _statuses.Where(ps => ps.PaymentId == paymentId)
            .ToListAsync();
    }

    public async Task CreateAsync(PaymentStatus paymentStatus)
    {
        await _statuses.AddAsync(paymentStatus);
    }
}