using Microsoft.EntityFrameworkCore.Storage;
using VC.Services.Application.Events;
using VC.Services.Common;
using VC.Services.Repositories;

namespace VC.Services.Infrastructure.Persistence;

internal class UnitOfWork(
    DatabaseContext dbContext,
    IDomainEventDispatcher domainEventDispatcher,
    IServicesRepository servicesRepository,
    IResourcesRepository resourcesRepository,
    ICategoriesRepository categoriesRepository) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    public IServicesRepository Services => servicesRepository;
    public IResourcesRepository Resources => resourcesRepository;
    public ICategoriesRepository Categories => categoriesRepository;

    public void Dispose() => dbContext.Dispose();

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null)
            throw new InvalidOperationException("Transaction already started!");

        _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await dbContext.SaveChangesAsync();
            
            if(_transaction is not null)
                await _transaction.CommitAsync();
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