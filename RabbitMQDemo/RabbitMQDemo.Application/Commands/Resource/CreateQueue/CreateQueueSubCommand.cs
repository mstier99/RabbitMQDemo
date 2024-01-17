namespace RabbitMQDemo.Application.Commands.Resource.CreateQueue;

public record CreateQueueSubCommand(
    string Name,
    bool Durable,
    bool Exclusive,
    bool AutoDelete,
    Dictionary<string, object>? Arguments);
