using System.Linq.Expressions;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Domain;

public interface IRepository
{
    Task AddAsync(Resource entity);

    Task<Resource> GetAsync(Guid id);

    Task Update(Resource entity);
}