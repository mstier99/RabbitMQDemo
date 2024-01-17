using RabbitMQDemo.Domain.Users;

namespace RabbitMQDemo.Domain.Vhosts;

public interface IVhost
{
    string Name { get; init; }
    RabbitMQUser Admin { get; init; }
    RabbitMQUser Consumer { get; init; }
    RabbitMQUser Publisher { get; init; }
}