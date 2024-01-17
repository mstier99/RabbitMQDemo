using RabbitMQDemo.Domain.Users;

namespace RabbitMQDemo.Domain.Vhosts;

public abstract class VhostOption
{
    public required RabbitMQUser Admin { get; set; }
    public required RabbitMQUser Publisher { get; set; }
    public required RabbitMQUser Consumer { get; set; }
}
