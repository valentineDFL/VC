using AutoMapper;

namespace VC.Services.Application.Common.Mappings;
public interface IMapWith<TEntity>
{
    void Mapping(Profile profile) =>
        profile.CreateMap(typeof(TEntity), GetType());
}
