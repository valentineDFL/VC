using FluentResults;
using MediatR;
using VC.Services.Repositories;
using VC.Services.UnitOfWork;

namespace VC.Services.Application.Services.Commands.UpdateService
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Result>
    {
        private readonly IServicesRepository _dbRepository;
        private readonly IDbSaver _dbSaver;
        
        public UpdateServiceCommandHandler(IServicesRepository dbRepository, IDbSaver saver)
        {
            _dbRepository = dbRepository;
            _dbSaver = saver;
        }

        public async Task<Result> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _dbRepository.GetByIdAsync(request.Id, cancellationToken);

            if (service == null)
                return Result.Fail("Not found");

            service.Title = request.Title;
            service.Description = request.Description;
            service.Price = request.Price;
            service.Duration = request.Duration;
            service.Category = request.Category;
            service.UpdatedAt = DateTime.UtcNow;
            service.IsActive = request.IsActive;
            service.ResourceRequirement = request.ResourceRequirement;

            await _dbRepository.Update(service, cancellationToken);
            await _dbSaver.SaveAsync();

            return Result.Ok();
        }
    }
}
