
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Application.Commands.Vhost;

public class CreateVhostCommandHandler : ICommandHandler<CreateVhostCommand>
{
    IManagementApiRepository _repo;

    public CreateVhostCommandHandler(IManagementApiRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> Handle(CreateVhostCommand request, CancellationToken cancellationToken)
    {
        var result = await _repo.PutVhostAsync(request);
        return result;  
    }
}
