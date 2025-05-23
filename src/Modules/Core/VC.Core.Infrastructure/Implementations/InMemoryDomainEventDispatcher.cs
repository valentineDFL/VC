using Microsoft.Extensions.DependencyInjection;
using VC.Core.Application.Events;
using VC.Core.Common;
using VC.Core.Events;

namespace VC.Core.Infrastructure.Implementations;

public class InMemoryDomainEventDispatcher(IServiceProvider _serviceProvider) : IDomainEventDispatcher
{
    public async Task DispatchAsync(DomainEvent domainEvent)
    {
        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = _serviceProvider.GetServices(handlerType).Where(h => h is not null);

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("HandleAsync") 
                         ?? throw new InvalidOperationException("HandleAsync method not found");
            
            var parameterType = method.GetParameters()[0].ParameterType;
            if (!parameterType.IsInstanceOfType(domainEvent))
            {
                throw new InvalidOperationException(
                    $"Handler {handler!.GetType()} expects {parameterType}, but got {eventType}"
                );
            }

            var result = method.Invoke(handler, [domainEvent]) 
                         ?? throw new InvalidOperationException("HandleAsync returned null");
            
            await (Task)result;
        }
    }
}