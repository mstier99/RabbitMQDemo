namespace RabbitMQDemo.Application.Commands.Resource.CreateExchange;

public record CreateExchangeSubCommand(
    string Name,
    string Type,
    bool Durable,
    bool AutoDelete
    );
