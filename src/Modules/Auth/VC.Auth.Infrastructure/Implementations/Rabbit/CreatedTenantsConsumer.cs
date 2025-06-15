using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VC.Auth.Models;
using VC.Auth.Repositories;
using VC.Shared.RabbitMQIntegration;
using VC.Shared.RabbitMQIntegration.Consumers.Interfaces;
using VC.Shared.Utilities.RabbitEnums;
using VC.Shared.Utilities.TenantServiceDtos;
using Microsoft.Extensions.DependencyInjection;

namespace VC.Auth.Infrastructure.Implementations.Rabbit;

internal class CreatedTenantsConsumer : IConsumer
{
    private readonly RabbitClient _rabbitClient;
    private IChannel _channel;

    private readonly IServiceScopeFactory _scopeFactory;
    
    public CreatedTenantsConsumer(RabbitClient rabbitClient,
                                  IServiceScopeFactory scopeFactory)
    {
        _rabbitClient = rabbitClient;
        _scopeFactory = scopeFactory;
    }

    public async Task OnConsumeAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        var consumer = (AsyncEventingBasicConsumer)sender;

        var body = eventArgs.Body.ToArray();
        var decodedTenant = RabbitCoder.DeserializeUTF8<TenantDto>(body);

        var scope = _scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var user = await userRepository.GetByIdAsync(decodedTenant.UserId);
        if (user is null)
            throw new NullReferenceException("Tenant have invalid user Id");

        user.TenantId = decodedTenant.Id;
        user.Permissions.Add(new Permission
        { 
            Id = Guid.CreateVersion7(),
            Name = Permissions.Tenant
        });

        await userRepository.UpdateAsync(user);

        await consumer.Channel.BasicAckAsync(eventArgs.DeliveryTag, false);
    }

    public async Task ConnectAsync(CancellationToken cts = default)
    {
        _channel = await _rabbitClient.CreateChannelAsync(cts);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += OnConsumeAsync;

        var queue = Queues.CreatedTenants.ToString();
        var exchange = Exchanges.CreatedTenantsDirect.ToString();
        var routingKey = RoutingKeys.CreatedTenantsKey.ToString();

        await _channel.QueueDeclareAsync(queue, true, false, false, null, cancellationToken: cts);
        await _channel.QueueBindAsync(queue, exchange, routingKey, null, cancellationToken: cts);

        await _channel.BasicConsumeAsync(queue, false, consumer, cts);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}