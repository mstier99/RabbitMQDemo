using RabbitMQDemo.Application.CQRS;

namespace RabbitMQDemo.Application.Commands.Resource.CreateQueue;

public record CreateQueuesCommand(List<CreateQueueSubCommand> Queues) : ICommand;
