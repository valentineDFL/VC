using Microsoft.EntityFrameworkCore;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Infrastructure.Repositories;

internal class ResourceRepository : IResourceRepository
{
    private readonly ResourceDbContext _dbContext;

    public ResourceRepository(ResourceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Resource entity)
        => await _dbContext.Resources.AddAsync(entity);

    public async Task<Resource?> GetAsync(Guid id)
        => await _dbContext.Resources
            .Include(r => r.Skills)
            .ThenInclude(s => s.Experience)
            .FirstOrDefaultAsync(r => r.Id == id);

    public void Update(Resource entity)
        => _dbContext.Resources.Update(entity);
}
