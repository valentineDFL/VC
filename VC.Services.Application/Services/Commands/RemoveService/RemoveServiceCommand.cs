using MediatR;

namespace VC.Services.Application.Services.Commands.RemoveService
{
    public class RemoveServiceCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
