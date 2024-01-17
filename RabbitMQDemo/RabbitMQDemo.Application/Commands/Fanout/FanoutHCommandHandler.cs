using MediatR;
using RabbitMQDemo.Application.CQRS;
using RabbitMQDemo.Domain.Response;
using RabbitMQDemo.Infra;

namespace RabbitMQDemo.Application.Messages.Fanout;

public class FanoutHCommandHandler : ICommandHandler<FanoutCommand>
{
    IRabbitMQConnection _connection;

    public FanoutHCommandHandler(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    public async Task<Result> Handle(FanoutCommand request, CancellationToken cancellationToken)
    {
        using var channel = _connection.CreateChannel();

        channel.BasicPublish(
            request.Exchange,
            routingKey: "",
            mandatory: false,
            basicProperties: null,
            request.BodyAsByteArray);

        return await Task.FromResult(Result.Success());
    }
}
