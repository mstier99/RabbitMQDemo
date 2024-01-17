using MediatR;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Application.CQRS;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>
{
}