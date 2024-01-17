namespace RabbitMQDemo.Application.Commands.Vhost;

public record CreateVhostCommand(string Name): ICommand;
