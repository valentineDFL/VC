using Microsoft.EntityFrameworkCore;
using VC.Services.Application.ServicesUseCases;
using VC.Services.Application.ServicesUseCases.Models;

namespace VC.Services.Infrastructure.Persistence.Queries;

public class GetServiceDetailsUseCase(DatabaseContext _dbContext) : IGetServiceDetailsUseCase
{
    public async Task<ServiceDetailsDto?> ExecuteAsync(Guid serviceId)
    {
        return await _dbContext.Services
            .Where(s => s.Id == serviceId)
            // Join с категориями (LEFT JOIN)
            .GroupJoin(
                _dbContext.Categories,
                service => service.CategoryId,
                category => category.Id,
                (service, categories) => new { Service = service, Categories = categories }
            )
            .SelectMany(
                temp => temp.Categories.DefaultIfEmpty(),
                (serviceData, category) => new { serviceData.Service, Category = category }
            )
            // Join с ресурсами (INNER JOIN по списку RequiredResources)
            .SelectMany(
                temp => _dbContext.Resources
                    .Where(r => temp.Service.RequiredResources.Contains(r.Id))
                    .DefaultIfEmpty(),  // Для LEFT JOIN
                (temp, resource) => new { temp.Service, temp.Category, Resource = resource }
            )
            // Группировка для агрегации ресурсов
            .GroupBy(
                x => new { x.Service, x.Category },
                x => x.Resource
            )
            .Select(g => new ServiceDetailsDto
            {
                Id = g.Key.Service.Id,
                Title = g.Key.Service.Title,
                Description = g.Key.Service.Description,
                BasePrice = g.Key.Service.BasePrice,
                BaseDuration = g.Key.Service.BaseDuration,
                Category = g.Key.Category != null 
                    ? new CategoryDto 
                    { 
                        Id = g.Key.Category.Id, 
                        Title = g.Key.Category.Title 
                    } 
                    : null,
                RequiredResources = g.Where(r => r != null).Select(r => new ResourceDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Count = (int)r.Count
                }).ToList(),
                IsActive = g.Key.Service.IsActive,
                EmployeeAssignments = g.Key.Service.EmployeeAssignments.Select(a => new EmployeeAssignmentDto
                {
                    EmployeeId = a.EmployeeId,
                    Price = a.Price,
                    Duration = a.Duration
                }).ToList()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}