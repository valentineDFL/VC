using Microsoft.EntityFrameworkCore;
using VC.Core.Common;
using VC.Core.Repositories;

namespace VC.Core.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> 
    where TEntity : AggregateRoot<TId>
    where TId : notnull
{
    protected readonly DatabaseContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(DatabaseContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public async Task<ICollection<TEntity>> GetByIdsAsync(
        IEnumerable<TId> ids,
        CancellationToken cancellationToken = default) =>
        await DbSet
            .Where(x => ids.Contains(x.Id))
            .ToArrayAsync(cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

    public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) 
        => await DbSet.ToArrayAsync(cancellationToken);

    public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default) 
        => await DbSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await DbSet.AddAsync(entity, cancellationToken);

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }
}