using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VC.Core.Repositories;
using VC.Shared.RabbitMQIntegration;
using VC.Shared.RabbitMQIntegration.Consumers.Interfaces;
using VC.Shared.Utilities.RabbitEnums;
using VC.Shared.Utilities.OrdersModuleDtos;
using VC.Core.Application;

namespace VC.Core.Infrastructure.Implementations.RabbitConsumers;

internal class CreatedOrdersConsumer : IConsumer
{
    private readonly RabbitClient _rabbitClient;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly OrderSnapshotFactory _historyFactory;

    private IChannel _channel;

    public CreatedOrdersConsumer(RabbitClient rabbitClient, IServiceScopeFactory scopeFactory, OrderSnapshotFactory historyFactory)
    {
        _rabbitClient = rabbitClient;
        _serviceScopeFactory = scopeFactory;
        _historyFactory = historyFactory;
    }

    public async Task OnConsumeAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        var bytes = eventArgs.Body.ToArray();
        var orderDto = RabbitCoder.DeserializeUTF8<OrderDetailsDto>(bytes);

        var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var orderSnapshot = _historyFactory.CreateByOrderDetail(orderDto);

        await unitOfWork.BeginTransactionAsync();
        await unitOfWork.OrdersHistory.AddAsync(orderSnapshot);
        await unitOfWork.CommitAsync();

        var consumer = (AsyncEventingBasicConsumer)sender;
        await consumer.Channel.BasicAckAsync(eventArgs.DeliveryTag, false);
    }

    public async Task ConnectAsync(CancellationToken cts = default)
    {
        _channel = await _rabbitClient.CreateChannelAsync(cts);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += OnConsumeAsync;

        var queue = Queues.Orders.ToString();

        await _channel.QueueDeclareAsync(queue, true, false, false, null);
        await _channel.QueueBindAsync(queue, 
                                      Exchanges.OrdersDirect.ToString(), 
                                      RoutingKeys.OrdersKey.ToString(), 
                                      cancellationToken: cts);

        await _channel.BasicConsumeAsync(queue, false, consumer);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }
}