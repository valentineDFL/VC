using System.Linq.Expressions;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Domain;

public interface IRepository
{
    public Task AddAsync(Resource entity);

    public Task<Resource> GetAsync(Guid id);

    public void Update(Resource entity);

    Task<bool> ExistsAsync(Expression<Func<Resource, bool>> predicate);
}