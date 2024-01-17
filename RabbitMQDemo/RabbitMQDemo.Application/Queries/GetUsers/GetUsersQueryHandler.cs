
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Application.Queries.GetUsers;

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, GetUsersResponse>
{
    IManagementApiRepository _repo;

    public GetUsersQueryHandler(IManagementApiRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetUsersResponse>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken) => await _repo.GetUsersAsync();
}
