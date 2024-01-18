using RabbitMQ.Client;
using RabbitMQDemo.Application.Commands.Resource.CreateBind;
using RabbitMQDemo.Application.Commands.Resource.CreateExchange;
using RabbitMQDemo.Application.Commands.Resource.CreateQueue;
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Infra.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly IRabbitMQConnection _connection;

    public ResourceRepository(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    public Result CreateQueues(CreateQueuesCommand command)
    {
        using var channel = _connection.CreateChannel();

        foreach (var queue in command.Queues)
        {
            channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete, queue.Arguments);
        }

        return Result.Success();
    }

    public Result CreateExchanges(CreateExchangesCommand command)
    {
        using var channel = _connection.CreateChannel();

        foreach (var exchange in command.Exchanges)
        {
            channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete);
        }

        return Result.Success();
    }

    public Result CreateBinds(CreateBindsCommand command)
    {
        using var channel = _connection.CreateChannel();

        foreach (var bind in command.Binds)
        {
            channel.QueueBind(bind.QueueName, bind.ExchangeName, bind.RoutingKey, arguments: null);
        }

        return Result.Success();
    }
}
