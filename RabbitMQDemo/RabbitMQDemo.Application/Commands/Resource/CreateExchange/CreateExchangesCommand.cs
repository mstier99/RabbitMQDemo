using RabbitMQDemo.Application.CQRS;

namespace RabbitMQDemo.Application.Commands.Resource.CreateExchange;

public record CreateExchangesCommand(List<CreateExchangeSubCommand> Exchanges) : ICommand;
