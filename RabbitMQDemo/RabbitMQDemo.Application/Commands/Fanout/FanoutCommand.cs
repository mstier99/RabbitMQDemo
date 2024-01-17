using RabbitMQDemo.Application.CQRS;
using RabbitMQDemo.Domain.Abstraction;

namespace RabbitMQDemo.Application.Messages.Fanout;

public sealed class FanoutCommand : Message, ICommand
{
    public FanoutCommand(string vhost, string exchange, string body)
        : base(vhost, exchange, body)
    {
    }
}
