using Microsoft.EntityFrameworkCore.Storage;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly OrdersDbContext _dbContext;
    private IDbContextTransaction _transaction;

    private readonly IOrdersRepository _ordersRepository;
    private readonly IPaymentsRepository _paymentsRepository;

    private readonly IOrdersStatusesRepository _ordersStatusesRepository;
    private readonly IPaymentsStatusesRepository _paymentsStatusesRepository;

    private readonly IOutboxMessagesRepository _outboxMessagesRepository;

    public UnitOfWork(OrdersDbContext dbContext,
                      IOrdersRepository ordersRepository,
                      IPaymentsRepository paymentsRepository,
                      IOrdersStatusesRepository ordersStatusesRepository,
                      IPaymentsStatusesRepository paymentsStatusesRepository,
                      IOutboxMessagesRepository outboxMessagesRepository)
    {
        _dbContext = dbContext;
        _ordersRepository = ordersRepository;
        _paymentsRepository = paymentsRepository;
        _ordersStatusesRepository = ordersStatusesRepository;
        _paymentsStatusesRepository = paymentsStatusesRepository;
        _outboxMessagesRepository = outboxMessagesRepository;
    }

    public IOrdersRepository Orders => _ordersRepository;

    public IPaymentsRepository Payments => _paymentsRepository;

    public IOrdersStatusesRepository OrdersStatuses => _ordersStatusesRepository;

    public IPaymentsStatusesRepository PaymentsStatuses => _paymentsStatusesRepository;

    public IOutboxMessagesRepository OutboxMessages => _outboxMessagesRepository;

    public async Task BeginTransactionAsync(CancellationToken cts = default)
    {
        if (_transaction is not null)
            throw new InvalidOperationException("The transaction in progress now");

        _transaction = await _dbContext.Database.BeginTransactionAsync(cts);
    }

    public async Task CommitAsync(CancellationToken cts = default)
    {
        try
        {
            await _dbContext.SaveChangesAsync(cts);

            if(_transaction is not null)
                await _transaction.CommitAsync(cts);
        }
        catch (Exception ex)
        {
            await RollbackAsync(cts);
            throw;
        }
        finally
        {
            if(_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackAsync(CancellationToken cts = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("The transaction is not in progress now");

        try
        {
            await _transaction.RollbackAsync();
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}