using FluentResults;
using VC.Recources.Application.Endpoints.Models.Requests;
using VC.Recources.Domain.Entities;

namespace VC.Recources.Application.Interfaces;

public interface IService
{
    public Task<Result> AddAsync(CreateDto dto);

    public Task<Result<Resource>> GetAsync(Guid id);

    public Task<Result> UpdateAsync(UpdateDto dto);
}