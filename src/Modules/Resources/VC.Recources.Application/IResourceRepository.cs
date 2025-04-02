using VC.Recources.Domain.Entities;

namespace VC.Recources.Application;

public interface IResourceRepository
{
    public Task AddAsync(Resource entity);

    public Task<Resource> GetAsync(Guid id);

    public void Update(Resource entity);
}
