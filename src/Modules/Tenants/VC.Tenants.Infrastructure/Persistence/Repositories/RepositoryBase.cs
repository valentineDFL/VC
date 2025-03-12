using Microsoft.EntityFrameworkCore;
using VC.Tenants.Repositories;

namespace VC.Tenants.Infrastructure.Persistence.Repositories;

/// <summary>
/// Базовый репозиторий.
/// </summary>
/// <typeparam name="TEntity">Доменная сущность.</typeparam>
internal class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> Query;
    protected readonly TenantsDbContext DbContext;

    public RepositoryBase(TenantsDbContext dbContext)
    {
        DbContext = dbContext;
        Query = DbContext.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await DbContext.AddAsync(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        DbContext.Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbContext.Update(entity);
    }
}