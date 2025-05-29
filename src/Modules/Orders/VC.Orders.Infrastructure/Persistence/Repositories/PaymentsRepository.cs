using Microsoft.EntityFrameworkCore;
using VC.Orders.Payments;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class PaymentsRepository : IPaymentsRepository
{
    private readonly OrdersDbContext _dbContext;
    private DbSet<Payment> _payments;

    public PaymentsRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
        _payments = _dbContext.Payments;
    }

    public async Task<Payment> GetByOrderIdAsync(Guid orderId)
        => await _payments.FirstOrDefaultAsync(p => p.OrderId == orderId);

    public async Task AddAsync(Payment payment)
        => await _payments.AddAsync(payment);

    public Task UpdateAsync(Payment payment)
    {
        _payments.Update(payment);

        return Task.CompletedTask;
    }
}