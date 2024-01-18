using MediatR;
using RabbitMQDemo.Application.CQRS;
using RabbitMQDemo.Application.Repositories;
using RabbitMQDemo.Domain.Response;

namespace RabbitMQDemo.Application.Commands.Resource.CreateExchange;

public class CreateExchangeCommandHandler : ICommandHandler<CreateExchangesCommand>
{
    IResourceRepository _repo;

    public CreateExchangeCommandHandler(IResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> Handle(CreateExchangesCommand request, CancellationToken cancellationToken)
    {
        var result = _repo.CreateExchanges(request);
        return await Task.FromResult(result);
    }
}
