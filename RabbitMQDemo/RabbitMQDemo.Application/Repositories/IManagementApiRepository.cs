using RabbitMQDemo.Application.Commands.User.CreatePermission;
using RabbitMQDemo.Application.Commands.User.CreateUser;
using RabbitMQDemo.Application.Commands.User.DeleteUser;
using RabbitMQDemo.Application.Commands.Vhost;
using RabbitMQDemo.Application.Queries.GetExchanges;
using RabbitMQDemo.Application.Queries.GetQueues;
using RabbitMQDemo.Application.Queries.GetUsers;
using RabbitMQDemo.Application.Queries.UsersAreExists;

namespace RabbitMQDemo.Application.Repositories
{
    public interface IManagementApiRepository
    {
        Task<Result> DeleteDefaultVhostAsync();
        Task<Result> DeleteGuestUserAsync();
        Task<Result> DeleteUsersAsync(DeleteUsersCommand command);

        Task<Result<GetUsersResponse>> GetUsersAsync();
        Task<Result<GetQueuesResponse>> GetQueuesAsync();
        Task<Result<GetExchangesResponse>> GetExchangesAsync();
        Task<Result<bool>> UsersAreExistsAsync(UsersAreExistsQuery query);

        Task<Result> PutPermissionAsync(CreatePermissionCommand command);
        Task<Result> PutUserAsync(CreateUserCommand command);
        Task<Result> PutVhostAsync(CreateVhostCommand command);
    }
}