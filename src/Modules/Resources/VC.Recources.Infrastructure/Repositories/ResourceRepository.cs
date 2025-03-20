using Microsoft.EntityFrameworkCore;

namespace VC.Recources.Infrastructure.Repositories;

internal class ResourceRepository : IResourceRepository
{
    private readonly ResourceDbContext _context;

    public ResourceRepository(ResourceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Resource.Domain.Entities.Resource entity)
        => await _context.Resources.AddAsync(entity);

    public async Task<Resource.Domain.Entities.Resource> GetAsync(Guid id)
        => await _context.Resources
            .Include(r => r.Skills)
            .ThenInclude(s => s.Expirience)
            .FirstOrDefaultAsync(r => r.Id == id);

    public void Update(Resource.Domain.Entities.Resource entity)
        => _context.Resources.Update(entity);
}
