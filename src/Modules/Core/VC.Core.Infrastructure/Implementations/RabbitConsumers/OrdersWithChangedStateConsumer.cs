using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using VC.Core.Repositories;
using VC.Shared.RabbitMQIntegration;
using VC.Shared.RabbitMQIntegration.Consumers.Interfaces;
using VC.Shared.Utilities.OrdersModuleDtos;
using VC.Shared.Utilities.RabbitEnums;

namespace VC.Core.Infrastructure.Implementations.RabbitConsumers;

internal class OrdersWithChangedStateConsumer : IConsumer
{
    private readonly RabbitClient _rabbitClient;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private IChannel _channel;

    public OrdersWithChangedStateConsumer(RabbitClient rabbitClient, IServiceScopeFactory scopeFactory)
    {
        _rabbitClient = rabbitClient;
        _serviceScopeFactory = scopeFactory;
    }

    public async Task ConnectAsync(CancellationToken cts = default)
    {
        _channel = await _rabbitClient.CreateChannelAsync(cts);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += OnConsumeAsync;

        var queue = Queues.ChangedOrders.ToString();
        var exchange = Exchanges.ChangedOrdersDirect.ToString();

        await _channel.QueueDeclareAsync(queue, true, false, false, null, cancellationToken: cts);
        await _channel.ExchangeDeclareAsync(exchange, ExchangeType.Direct, true, false, cancellationToken: cts);
        await _channel.QueueBindAsync(queue,
                                      exchange,
                                      RoutingKeys.ChangedOrdersKey.ToString(),
                                      cancellationToken: cts);

        await _channel.BasicConsumeAsync(Queues.ChangedOrders.ToString(), false, consumer);
    }

    public async Task OnConsumeAsync(object sender, BasicDeliverEventArgs eventArgs)
    {
        var consumer = (AsyncEventingBasicConsumer)sender;
        var body = eventArgs.Body.ToArray();

        var orderDetailDto = RabbitCoder.DeserializeUTF8<OrderDetailsDto>(body);

        var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var orderSnapshot = await unitOfWork.OrdersHistory.GetByOrderIdAsync(orderDetailDto.Id);
        if (orderSnapshot is null)
        {
            await consumer.Channel.BasicAckAsync(eventArgs.DeliveryTag, false);
            throw new NullReferenceException("OrderSnapshot not found!");
        }

        var orderDtoState = ParseSharedOrderStateToServices(orderDetailDto.State);
        orderSnapshot.ChangeOrderState(orderDtoState);

        await unitOfWork.CommitAsync();

        await consumer.Channel.BasicAckAsync(eventArgs.DeliveryTag, false);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }

    private Services.OrderState ParseSharedOrderStateToServices(OrderState state)
    {
        if (state is OrderState.Canceled ||
            state is OrderState.Refunded)
            return Services.OrderState.Canceled;

        else if (state is OrderState.Created ||
                 state is OrderState.Accepted ||
                 state is OrderState.Paused ||
                 state is OrderState.Paid)
            return Services.OrderState.Pending;

        else if (state is OrderState.Executed)
            return Services.OrderState.Executed;

        else
            return Services.OrderState.Executing;
    }
}