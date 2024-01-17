
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Application.Commands.User.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    IManagementApiRepository _repo;

    public CreateUserCommandHandler(IManagementApiRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken) 
        => await _repo.PutUserAsync(request);
}
