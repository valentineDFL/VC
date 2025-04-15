using FluentResults;
using MediatR;
using VC.Services.Entities;
using VC.Services.Repositories;
using VC.Services.UnitOfWork;

namespace VC.Services.Application.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Result<Guid>>
{
    private readonly IServicesRepository _dbRepository;
    private readonly IDbSaver _dbSaver;

    public CreateServiceCommandHandler(IServicesRepository dbRepository, IDbSaver dbSaver)
    {
        _dbRepository = dbRepository;
        _dbSaver = dbSaver;
    }

    public async Task<Result<Guid>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = new Service
        {
            Id = Guid.CreateVersion7(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Duration = request.Duration,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            ResourceRequirement = request.ResourceRequirement // не знаю кстати если он добавит там ресурсы какие то то хз тут вроде должен быть запрос или тип того какие он добавил
        };

        await _dbRepository.AddAsync(service, cancellationToken);
        await _dbSaver.SaveAsync();

        return Result.Ok(service.Id);
    }
}
