using RabbitMQDemo.Application.CQRS;
using RabbitMQDemo.Application.Repositories;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Application.Commands.Resource.CreateBind;

public class CreateBindsCommandHandler : ICommandHandler<CreateBindsCommand>
{
    IResourceRepository _repo;

    public CreateBindsCommandHandler(IResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> Handle(CreateBindsCommand request, CancellationToken cancellationToken)
    {
        var result = _repo.CreateBinds(request);
        return await Task.FromResult(result);
    }
}
