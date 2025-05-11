using VC.Core.Common;
using VC.Core.Services;

namespace VC.Core.Events;

public class ServiceCreatedEvent : DomainEvent
{
    public ServiceCreatedEvent(Service service)
        => Service = service;
    
    public Service Service { get; }
}