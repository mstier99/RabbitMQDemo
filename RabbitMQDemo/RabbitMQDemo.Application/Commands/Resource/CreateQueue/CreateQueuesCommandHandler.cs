using RabbitMQDemo.Application.CQRS;
using RabbitMQDemo.Application.Repositories;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Application.Commands.Resource.CreateQueue;

public class CreateQueuesCommandHandler : ICommandHandler<CreateQueuesCommand>
{
    IResourceRepository _repo;

    public CreateQueuesCommandHandler(IResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> Handle(CreateQueuesCommand request, CancellationToken cancellationToken)
    {
        var result = _repo.CreateQueues(request.Queues);
        return await Task.FromResult(result);
    }
}
