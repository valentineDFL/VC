namespace VC.Recources;

public interface IResourceRepository
{
    public Task AddAsync(Resource.Domain.Entities.Resource entity);

    public Task<Resource.Domain.Entities.Resource> GetAsync(Guid id);

    public void Update(Resource.Domain.Entities.Resource entity);
}
