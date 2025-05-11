using Microsoft.EntityFrameworkCore.Storage;
using VC.Core.Application.Events;
using VC.Core.Common;
using VC.Core.Repositories;

namespace VC.Core.Infrastructure.Persistence;

internal class UnitOfWork(
    DatabaseContext dbContext,
    IDomainEventDispatcher domainEventDispatcher,
    IServicesRepository servicesRepository,
    IResourcesRepository resourcesRepository,
    ICategoriesRepository categoriesRepository,
    IEmployeesRepository employeesRepository,
    IWorkSchedulesRepository workSchedulesRepository) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    public IServicesRepository Services => servicesRepository;
    public IResourcesRepository Resources => resourcesRepository;
    public ICategoriesRepository Categories => categoriesRepository;
    public IEmployeesRepository Employees => employeesRepository;
    public IWorkSchedulesRepository WorkSchedules => workSchedulesRepository;

    public void Dispose() => dbContext.Dispose();

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null)
            throw new InvalidOperationException("Transaction already started!");

        _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            
            if(_transaction is not null)
                await _transaction.CommitAsync(cancellationToken);
            await DispatchDomainEvents();
        }
        catch (Exception)
        {
            await RollBackAsync();
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

    public async Task RollBackAsync()
    {
        if (_transaction is null)
            throw new InvalidOperationException("Transaction must be in progress");

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
    
    private async Task DispatchDomainEvents()
    {
        var aggregates = dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>();
        foreach (var aggregate in aggregates)
        {
            foreach (var domainEvent in aggregate.Entity.DomainEvents)
                await domainEventDispatcher.DispatchAsync(domainEvent);
            
            aggregate.Entity.ClearDomainEvents();
        }
    }
}