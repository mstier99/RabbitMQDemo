using MediatR;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Application.CQRS;

public interface IQueryHandler<TQuery, TResponse>: IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
