
using RabbitMQDemo.Application.Repositories;

namespace RabbitMQDemo.Application.Queries.GetExchanges;

public class GetExchangesQueryHandler : IQueryHandler<GetExchangesQuery, GetExchangesResponse>
{
    IManagementApiRepository _repo;

    public GetExchangesQueryHandler(IManagementApiRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<GetExchangesResponse>> Handle(
        GetExchangesQuery request,
        CancellationToken cancellationToken) => await _repo.GetExchangesAsync();
}
