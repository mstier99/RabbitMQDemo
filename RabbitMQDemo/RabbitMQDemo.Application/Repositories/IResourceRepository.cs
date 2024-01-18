using RabbitMQDemo.Application.Commands.Resource.CreateBind;
using RabbitMQDemo.Application.Commands.Resource.CreateExchange;
using RabbitMQDemo.Application.Commands.Resource.CreateQueue;

namespace RabbitMQDemo.Application.Repositories;

public interface IResourceRepository
{
    Result CreateQueues(CreateQueuesCommand command);
    Result CreateExchanges(CreateExchangesCommand command);
    Result CreateBinds(CreateBindsCommand command);
}
