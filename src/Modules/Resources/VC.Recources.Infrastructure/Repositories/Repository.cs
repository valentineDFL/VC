using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Infrastructure.Repositories;

internal class Repository : IRepository
{
    private readonly DbSet<Resource> _resources;

    public Repository(DbContext.DbContext dbContext)
    {
        _resources = dbContext.Set<Resource>();
    }

    public async Task AddAsync(Resource entity)
        => await _resources.AddAsync(entity);

    public async Task<Resource> GetAsync(Guid id)
        => await _resources
            .Include(r => r.Skills)
            .ThenInclude(s => s.Experience)
            .FirstOrDefaultAsync(r => r.Id == id);

    public void Update(Resource entity)
        => _resources.Update(entity);

    // удалить
    public async Task<bool> ExistsAsync(Expression<Func<Resource, bool>> predicate)
        => await _resources.AnyAsync(predicate);
}