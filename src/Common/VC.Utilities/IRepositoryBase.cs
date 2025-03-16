namespace VC.Tenants.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    public Task AddAsync(TEntity entity);

    public void Remove(TEntity entity);

    public void Update(TEntity entity);
}