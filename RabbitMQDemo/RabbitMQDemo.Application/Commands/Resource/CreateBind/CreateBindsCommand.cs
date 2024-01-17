using RabbitMQDemo.Application.CQRS;

namespace RabbitMQDemo.Application.Commands.Resource.CreateBind;

public record CreateBindsCommand(List<CreateBindSubCommand> Binds) : ICommand;
