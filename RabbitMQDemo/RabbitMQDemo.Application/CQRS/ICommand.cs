namespace RabbitMQDemo.Application.CQRS;

public interface ICommand : IRequest<Result>, ICommandBase
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, ICommandBase
{
}

public interface ICommandBase
{
}