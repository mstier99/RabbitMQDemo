
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Application.Queries.GetQueues;

public class GetQueuesQueryHandler : IQueryHandler<GetQueuesQuery, GetQueuesResponse>
{
    IManagementApiRepository _repo;

    public GetQueuesQueryHandler(IManagementApiRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetQueuesResponse>> Handle(
        GetQueuesQuery request, 
        CancellationToken cancellationToken) => await _repo.GetQueuesAsync();
}
