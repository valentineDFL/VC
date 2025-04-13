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
            var entity = await _dbRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
                return Result.Fail("Not found");

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.Duration = request.Duration;
            entity.Category = request.Category;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsActive = request.IsActive;
            entity.ResourceRequirement = request.ResourceRequirement;

            await _dbRepository.Update(entity, cancellationToken);
            await _dbSaver.SaveAsync();

            return Result.Ok();
        }
    }
}
