using Microsoft.EntityFrameworkCore;
using VC.Core.Application.ServicesUseCases;
using VC.Core.Application.ServicesUseCases.Models;

namespace VC.Core.Infrastructure.Persistence.Queries;

public class GetServiceDetailsUseCase(DatabaseContext _dbContext) : IGetServiceDetailsUseCase
{
    public async Task<ServiceDetailsDto?> ExecuteAsync(Guid serviceId)
    {
        return await _dbContext.Services
            .Where(s => s.Id == serviceId)
            .Select(s => new ServiceDetailsDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                BasePrice = s.BasePrice,
                BaseDuration = s.BaseDuration,
                Category = s.CategoryId != null 
                    ? _dbContext.Categories
                        .Where(c => c.Id == s.CategoryId)
                        .Select(c => new CategoryDto 
                        { 
                            Id = c.Id, 
                            Title = c.Title 
                        })
                        .FirstOrDefault() 
                    : null,
                RequiredResources = _dbContext.Resources
                    .Where(r => s.RequiredResources.Contains(r.Id))
                    .Select(r => new ResourceDto
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Count = r.Count
                    })
                    .ToList(),
                IsActive = s.IsActive,
                EmployeeAssignments = s.EmployeeAssignments
                    .Select(a => new EmployeeAssignmentDto
                    {
                        EmployeeId = a.EmployeeId,
                        Price = a.Price,
                        Duration = a.Duration
                    })
                    .ToList()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}