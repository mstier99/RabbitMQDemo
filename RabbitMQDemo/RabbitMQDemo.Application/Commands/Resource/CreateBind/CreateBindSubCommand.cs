namespace RabbitMQDemo.Application.Commands.Resource.CreateBind;

public record CreateBindSubCommand(
    string QueueName,
    string ExchangeName,
    string RoutingKey
    );
