using FluentResults;
using MediatR;

namespace VC.Services.Application.Services.Commands.RemoveService
{
    public class RemoveServiceCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
