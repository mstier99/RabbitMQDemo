using RabbitMQDemo.Application.Commands.Resource.CreateBind;
using RabbitMQDemo.Application.Commands.Resource.CreateExchange;
using RabbitMQDemo.Application.Commands.Resource.CreateQueue;

namespace RabbitMQDemo.Application.Repositories;

public interface IResourceRepository
{
    Result CreateQueues(List<CreateQueueSubCommand> queues);
    Result CreateExchanges(List<CreateExchangeSubCommand> exchanges);
    Result CreateBinds(List<CreateBindSubCommand> binds);
}
