using MediatR;
using VC.Services.Repositories;
using VC.Services.UnitOfWork;

namespace VC.Services.Application.Services.Commands.RemoveService;

public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand>
{
    public readonly IServicesRepository _dbRepository;
    public readonly IDbSaver _dbSaver;

    public RemoveServiceCommandHandler(IServicesRepository dbRepository, IDbSaver dbSaver)
    {
        _dbRepository = dbRepository;
        _dbSaver = dbSaver;
    }

    public async Task Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbRepository.GetByIdAsync(request.Id, cancellationToken);

        await _dbRepository.Remove(entity, cancellationToken);
        await _dbSaver.SaveAsync();
    }
}
