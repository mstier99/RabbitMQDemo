using RabbitMQ.Client;
using RabbitMQDemo.Application.Commands.Resource.CreateBind;
using RabbitMQDemo.Application.Commands.Resource.CreateExchange;
using RabbitMQDemo.Application.Commands.Resource.CreateQueue;
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Infra.Repositories.Resource;

public class ResourceRepository : IResourceRepository
{
    private readonly IRabbitMQConnection _connection;

    public ResourceRepository(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    public Result CreateQueues(List<CreateQueueSubCommand> queues)
    {
        using var channel = _connection.CreateChannel();

        foreach (var queue in queues)
        {
            channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
        }

        return Result.Success();
    }

    public Result CreateExchanges(List<CreateExchangeSubCommand> exchanges)
    {
        using var channel = _connection.CreateChannel();

        foreach (var exchange in exchanges)
        {
            channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete);
        }

        return Result.Success();
    }

    public Result CreateBinds(List<CreateBindSubCommand> binds)
    {
        using var channel = _connection.CreateChannel();

        foreach (var bind in binds)
        {
            channel.QueueBind(bind.QueueName, bind.ExchangeName, bind.RoutingKey, arguments: null);
        }

        return Result.Success();
    }
}
