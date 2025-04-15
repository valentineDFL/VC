using FluentResults;
using MediatR;
using VC.Services.Repositories;
using VC.Services.UnitOfWork;

namespace VC.Services.Application.Services.Commands.RemoveService;

public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand, Result>
{
    public readonly IServicesRepository _dbRepository;
    public readonly IDbSaver _dbSaver;

    public RemoveServiceCommandHandler(IServicesRepository dbRepository, IDbSaver dbSaver)
    {
        _dbRepository = dbRepository;
        _dbSaver = dbSaver;
    }

    public async Task<Result> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _dbRepository.GetByIdAsync(request.Id, cancellationToken);

        if (service == null)
            return Result.Fail("Not found service");

        await _dbRepository.Remove(service, cancellationToken);
        await _dbSaver.SaveAsync();

        return Result.Ok();
    }
}
